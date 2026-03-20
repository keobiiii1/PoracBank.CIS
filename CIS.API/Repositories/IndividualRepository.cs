using AutoMapper;
using CIS.API.Data;
using CIS.Assets.DTO;
using CIS.Assets.Enum;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;
using static CIS.Assets.DTO.IndividualInfoDTO.PageModel;

namespace CIS.API.Repositories;

public class IndividualRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public IndividualRepository(
        IMapper mapper,
        IDbContextFactory<CISDbContext> dbContextFactory,
        ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task<long> IndividualFullRegistrationAsync(IndividualRegistrationRequest request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            // 1. Create the Master Customer Record
            var customer = new Customer
            {
                EntityType = EntityType.Individual,
                CustomerCategory = request.Individual.CustomerCategory,
                CustomerType = request.Individual.CustomerType,
                CIDNumber = request.Individual.CIDNumber,
                CreatedAt = DateTime.UtcNow
            };
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            long customerId = customer.CustomerID;

            // 2. Sync generated CustomerID to sub-models for correct Foreign Key mapping
            request.Individual.CustomerID = customerId;
            request.Identification.CustomerID = customerId;
            request.Employment.CustomerID = customerId;

            // 3. Save Core Info (IndividualInfo)
            var individualInfo = _mapper.Map<IndividualInfo>(request.Individual);
            _db.IndividualInfos.Add(individualInfo);

            // 4. Save Identification & Employment
            _db.IndividualIdentifications.Add(_mapper.Map<IndividualIdentification>(request.Identification));
            _db.IndividualEmployments.Add(_mapper.Map<IndividualEmployment>(request.Employment));

            // 5. Save Family (Manual mapping ensures data is copied even if AutoMapper fails)
            _db.IndividualFamilies.Add(new IndividualFamily
            {
                CustomerID = customerId,
                SpouseGivenName = request.Family.SpouseGivenName,
                SpouseLastName = request.Family.SpouseLastName,
                SpouseMiddleName = request.Family.SpouseMiddleName,
                MotherMaidenGivenName = request.Family.MotherMaidenGivenName,
                MotherMaidenLastName = request.Family.MotherMaidenLastName,
                MotherMaidenMiddleName = request.Family.MotherMaidenMiddleName
            });

            // 6. Save Foreigner Info (if applicable)
            if (request.Individual.IsForeigner)
            {
                _db.IndividualForeigners.Add(new IndividualForeigner
                {
                    CustomerID = customerId,
                    PassportIDNumber = request.Individual.PassportIDNumber,
                    PassportExpiry = request.Individual.PassportExpiry,
                    IsACR = request.Individual.IsACR,
                    IsSIRV = request.Individual.IsSIRV,
                    IsSRRV = request.Individual.IsSRRV
                });
            }

            // 7. Save Address & Contact
            // ContactPerson, OfficePhoneNo, EmailAddress from AccountContact (step 8) merged here
            var address = _mapper.Map<Address>(request.Address);
            address.EntityID = customerId;
            address.EntityType = EntityType.Individual;
            _db.Addresses.Add(address);

            _db.Contacts.Add(new Contact
            {
                EntityID = customerId,
                EntityType = EntityType.Individual,
                HomePhoneNumber = request.Individual.HomePhoneNumber,
                MobilePhoneNumber = !string.IsNullOrEmpty(request.Individual.MobilePhoneNumber)
                    ? request.Individual.MobilePhoneNumber
                    : request.AccountContact.OfficePhoneNo,
                EmailAddress = !string.IsNullOrEmpty(request.Individual.EmailAddress)
                    ? request.Individual.EmailAddress
                    : request.AccountContact.EmailAddress,
                ContactPerson = request.AccountContact.ContactPerson
            });

            // 8. Save Account Purpose (from AccountContact step — BusinessInfoDTO)
            _db.AccountPurposes.Add(new AccountPurpose
            {
                EntityID = customerId,
                EntityType = EntityType.Individual,
                PurposeOfAccount = request.AccountContact.AccountPurpose,
                PurposeOfAccountOther = request.AccountContact.AccountPurposeOther,
                ProductsAvailed = request.AccountContact.ProductsAvailed,
                ProductsAvailedOther = request.AccountContact.ProductsAvailedOther
            });

            // 9. Save Business Interests List (Manual mapping prevents AutoMapper Exceptions)
            if (request.Individual.BusinessInterests != null)
            {
                foreach (var biz in request.Individual.BusinessInterests)
                {
                    _db.BusinessInterests.Add(new BusinessInterest
                    {
                        CustomerID = customerId,
                        EntityType = EntityType.Individual,
                        BusinessName = biz.BusinessName,
                        OwnershipPercentage = biz.OwnershipPercentage,
                    });
                }
            }

            // 10. Save Government Relations List
            if (request.Individual.GovRelatives != null)
            {
                foreach (var rel in request.Individual.GovRelatives)
                {
                    _db.GovernmentRelations.Add(new GovernmentRelation
                    {
                        CustomerID = customerId,
                        EntityType = EntityType.Individual,
                        Name = rel.Name,
                        Relationship = rel.Relationship,
                        HighestPositionOccupied = rel.Position,
                        PeriodCovered = rel.PeriodCovered
                    });
                }
            }

            // 11. Save Business Info (Step 7 — BusinessInformation component)
            if (!string.IsNullOrEmpty(request.Business.NameOfBusiness))
            {
                var bizInfo = _mapper.Map<BusinessInfo>(request.Business);
                bizInfo.CustomerID = customerId;
                _db.BusinessInfos.Add(bizInfo);
            }

            // 12. Save Beneficiary (if any)
            if (!string.IsNullOrEmpty(request.Beneficiary.BeneficiaryName))
            {
                var beneficiary = _mapper.Map<Beneficiary>(request.Beneficiary);
                beneficiary.CustomerID = customerId;
                beneficiary.EntityType = EntityType.Individual;
                _db.Beneficiaries.Add(beneficiary);
            }

            // 13. Save Acknowledgement (Signature)
            var ack = _mapper.Map<ClientAcknowlegdement>(request.Acknowledgement);
            ack.CustomerID = customerId;
            ack.EntityType = EntityType.Individual;
            _db.ClientAcknowlegdements.Add(ack);

            // 14. Save Bank Review
            var bankReview = _mapper.Map<BankReview>(request.BankReview);
            bankReview.CustomerID = customerId;
            bankReview.EntityType = EntityType.Individual;
            _db.BankReviews.Add(bankReview);

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();

            return customerId;
        }
        catch (Exception)
        {
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
    }

    public async Task<IndividualRegistrationRequest> GetFullRegistrationDetailsAsync(long customerId)
    {
        using var _db = _dbContextFactory.CreateDbContext();

        // 1. Table: Customers (Master record for Meta-data)
        var customer = await _db.Customers.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        if (customer == null) return null;

        var response = new IndividualRegistrationRequest();

        // 2. Table: IndividualInfos (Core Personal Data)
        var infoModel = await _db.IndividualInfos.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        response.Individual = _mapper.Map<IndividualInfoDTO.PageModel>(infoModel);

        if (response.Individual != null)
        {
            // Sync Step 1 & Meta Data from Customer Table
            response.Individual.CustomerCategory = customer.CustomerCategory;
            response.Individual.CustomerType = customer.CustomerType;
            response.Individual.CIDNumber = customer.CIDNumber;

            // 3. Table: BusinessInterests (Dynamic List)
            response.Individual.BusinessInterests = await _db.BusinessInterests
                .Where(x => x.CustomerID == customerId)
                .Select(x => new IndividualInfoDTO.PageModel.BusinessInterestModel
                {
                    BusinessName = x.BusinessName,
                    OwnershipPercentage = x.OwnershipPercentage
                }).ToListAsync();

            // 4. Table: GovernmentRelations (Dynamic List)
            response.Individual.GovRelatives = await _db.GovernmentRelations
                .Where(x => x.CustomerID == customerId)
                .Select(x => new IndividualInfoDTO.PageModel.GovRelativeModel
                {
                    Name = x.Name,
                    Relationship = x.Relationship,
                    Position = x.HighestPositionOccupied,
                    PeriodCovered = x.PeriodCovered
                }).ToListAsync();
        }

        // 5. Table: Addresses
        var addr = await _db.Addresses.AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntityID == customerId && e.EntityType == EntityType.Individual);
        response.Address = _mapper.Map<AddressDTO.PageModel>(addr);

        // 6. Table: IndividualIdentifications
        var ident = await _db.IndividualIdentifications.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        response.Identification = _mapper.Map<IndividualIdentificationDTO.PageModel>(ident);

        // 7. Table: IndividualEmployments
        var employ = await _db.IndividualEmployments.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        response.Employment = _mapper.Map<IndividualEmploymentDTO.PageModel>(employ);

        // 8. Table: IndividualFamilies
        var family = await _db.IndividualFamilies.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        response.Family = _mapper.Map<IndividualFamilyDTO.PageModel>(family);

        // 9. Table: IndividualForeigners (Optional)
        var foreigner = await _db.IndividualForeigners.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        response.Foreigner = _mapper.Map<IndividualForeignerDTO.PageModel>(foreigner);

        // 10. Table: Contacts & AccountPurposes (Combined logic)
        var contact = await _db.Contacts.AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntityID == customerId && e.EntityType == EntityType.Individual);
        response.Contact = _mapper.Map<ContactDTO.PageModel>(contact);

        var purpose = await _db.AccountPurposes.AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntityID == customerId && e.EntityType == EntityType.Individual);
        if (purpose != null)
        {
            response.AccountPurpose = _mapper.Map<AccountPurposeDTO.PageModel>(purpose);
        }

        return response;
    }
}