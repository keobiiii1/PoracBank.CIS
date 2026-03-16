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

    public async Task UpsertInfoAsync(IndividualInfoDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            if (request.CustomerID == 0)
            {
                var newCustomer = new Customer
                {
                    EntityType = EntityType.Individual,
                    CustomerCategory = request.CustomerCategory,
                    CustomerType = request.CustomerType,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Customers.Add(newCustomer);
                await _db.SaveChangesAsync(); // SQL generates the ID here
                request.CustomerID = newCustomer.CustomerID;
            }

            // --- 1. Main Individual Info ---
            var info = await _db.IndividualInfos.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (info == null)
            {
                info = _mapper.Map<IndividualInfo>(request);
                info.CustomerID = request.CustomerID;
                _db.IndividualInfos.Add(info);
            }
            else
            {
                _mapper.Map(request, info);
                _db.IndividualInfos.Update(info);
            }

            // --- 2. Individual Foreigner (Step 2) ---
            var foreigner = await _db.IndividualForeigners.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (request.IsForeigner) // Check your DTO property name
            {
                if (foreigner == null)
                {
                    foreigner = _mapper.Map<IndividualForeigner>(request);
                    foreigner.CustomerID = request.CustomerID;
                    _db.IndividualForeigners.Add(foreigner);
                }
                else
                {
                    _mapper.Map(request, foreigner);
                    _db.Entry(foreigner).Property(x => x.ForeignerID).IsModified = false;
                    _db.IndividualForeigners.Update(foreigner);
                }
            }
            else if (foreigner != null)
            {
                _db.IndividualForeigners.Remove(foreigner);
            }

            // --- 3. Individual Family (Step 5) ---
            var family = await _db.IndividualFamilies.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (family == null)
            {
                family = _mapper.Map<IndividualFamily>(request);
                family.CustomerID = request.CustomerID;
                _db.IndividualFamilies.Add(family);
            }
            else
            {
                _mapper.Map(request, family);
                _db.Entry(family).Property(x => x.FamilyID).IsModified = false;
                _db.IndividualFamilies.Update(family);
            }

            // --- 4. Account Purpose (Step 6) ---
            var acc = await _db.AccountPurposes.FirstOrDefaultAsync(e =>
                e.EntityID == request.CustomerID && e.EntityType == EntityType.Individual);

            if (acc == null)
            {
                acc = new AccountPurpose
                {
                    EntityID = request.CustomerID,
                    EntityType = EntityType.Individual,
                    PurposeOfAccount = request.AccountPurpose,
                    PurposeOfAccountOther = request.AccountPurposeOther,
                    ProductsAvailed = request.ProductsAvailed,
                    ProductsAvailedOther = request.ProductsAvailedOther
                };
                _db.AccountPurposes.Add(acc);
            }
            else
            {
                acc.PurposeOfAccount = request.AccountPurpose;
                acc.PurposeOfAccountOther = request.AccountPurposeOther;
                acc.ProductsAvailed = request.ProductsAvailed;
                acc.ProductsAvailedOther = request.ProductsAvailedOther;

                _db.Entry(acc).Property(x => x.AccountPurposeID).IsModified = false;
                _db.AccountPurposes.Update(acc);
            }

            // --- 5. Government Relations (Step 6 - Dynamic List) ---
            var oldRels = await _db.GovernmentRelations.Where(x => x.CustomerID == request.CustomerID).ToListAsync();
            _db.GovernmentRelations.RemoveRange(oldRels);

            if (request.HasGovRelative && request.GovRelatives != null)
            {
                foreach (var relReq in request.GovRelatives)
                {
                    _db.GovernmentRelations.Add(new GovernmentRelation
                    {
                        CustomerID = request.CustomerID,
                        Name = relReq.Name,
                        Relationship = relReq.Relationship,
                        HighestPositionOccupied = relReq.Position,
                        PeriodCovered = relReq.PeriodCovered
                    });
                }
            }

            // --- 6. Business Interests (Step 6 - Dynamic List) ---
            var oldBiz = await _db.BusinessInterests.Where(x => x.CustomerID == request.CustomerID).ToListAsync();
            _db.BusinessInterests.RemoveRange(oldBiz);

            if (request.BusinessInterests != null)
            {
                foreach (var bizReq in request.BusinessInterests)
                {
                    _db.BusinessInterests.Add(new BusinessInterest
                    {
                        CustomerID = request.CustomerID,
                        BusinessName = bizReq.BusinessName,
                        OwnershipPercentage = bizReq.OwnershipPercentage
                    });
                }
            }

            var contact = await _db.Contacts.FirstOrDefaultAsync(e =>
                e.EntityID == request.CustomerID && e.EntityType == EntityType.Individual);

            if (contact == null)
            {
                contact = new Contact
                {
                    EntityID = request.CustomerID,
                    EntityType = EntityType.Individual,
                    HomePhoneNumber = request.HomePhoneNumber,
                    MobilePhoneNumber = request.MobilePhoneNumber,
                    EmailAddress = request.EmailAddress
                };
                _db.Contacts.Add(contact);
            }
            else
            {
                contact.HomePhoneNumber = request.HomePhoneNumber;
                contact.MobilePhoneNumber = request.MobilePhoneNumber;
                contact.EmailAddress = request.EmailAddress;

                _db.Entry(contact).Property(x => x.ContactID).IsModified = false;
                _db.Contacts.Update(contact);
            }

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch (Exception ex)
        {
            if (tx != null) await tx.RollbackAsync();
            throw; // Consider logging ex here
        }
    }

    // --- Step 3: Identification ---
    public async Task UpsertIdentificationAsync(IndividualIdentificationDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        var old = await _db.IndividualIdentifications
            .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

        if (old == null)
        {
            _db.IndividualIdentifications.Add(_mapper.Map<IndividualIdentification>(request));
        }
        else
        {
            _mapper.Map(request, old);
            _db.IndividualIdentifications.Update(old);
        }
        await _db.SaveChangesAsync();
    }

    // --- Step 4: Employment ---
    public async Task UpsertEmploymentAsync(IndividualEmploymentDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();

        if (request.CustomerID <= 0)
            throw new Exception("CustomerID is required for employment upsert.");

        var old = await _db.IndividualEmployments
            .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

        if (old == null)
        {
            _db.IndividualEmployments.Add(_mapper.Map<IndividualEmployment>(request));
        }
        else
        {
            _mapper.Map(request, old);
            _db.IndividualEmployments.Update(old);
        }

        await _db.SaveChangesAsync();
    }

    // --- Step 5: Family ---
    public async Task UpsertFamilyAsync(IndividualFamilyDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.IndividualFamilies
                .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

            if (old == null)
            {
                _db.IndividualFamilies.Add(_mapper.Map<IndividualFamily>(request));
            }
            else
            {
                _mapper.Map(request, old);
                _db.IndividualFamilies.Update(old);
            }

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch
        {
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
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
            var address = _mapper.Map<Address>(request.Address);
            address.EntityID = customerId;
            address.EntityType = EntityType.Individual;
            _db.Addresses.Add(address);

            _db.Contacts.Add(new Contact
            {
                EntityID = customerId,
                EntityType = EntityType.Individual,
                EmailAddress = request.Individual.EmailAddress,
                MobilePhoneNumber = request.Individual.MobilePhoneNumber,
                HomePhoneNumber = request.Individual.HomePhoneNumber
            });

            // 8. Save Account Purpose
            _db.AccountPurposes.Add(new AccountPurpose
            {
                EntityID = customerId,
                EntityType = EntityType.Individual,
                PurposeOfAccount = request.Individual.AccountPurpose,
                PurposeOfAccountOther = request.Individual.AccountPurposeOther,
                ProductsAvailed = request.Individual.ProductsAvailed
            });

            // 9. Save Business Interests List (Manual mapping prevents AutoMapper Exceptions)
            if (request.Individual.BusinessInterests != null)
            {
                foreach (var biz in request.Individual.BusinessInterests)
                {
                    _db.BusinessInterests.Add(new BusinessInterest
                    {
                        CustomerID = customerId,
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
                        Name = rel.Name,
                        Relationship = rel.Relationship,
                        HighestPositionOccupied = rel.Position,
                        PeriodCovered = rel.PeriodCovered
                    });
                }
            }

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
