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

    public async Task<(long CustomerId, string CIDNumber)> IndividualFullRegistrationAsync(IndividualRegistrationRequest request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            // 1. Create the Master Customer Record
            // Generate unique 10-digit CIDNumber: IND + 7 digits (e.g. IND0000001)
            var cidNumber = await GenerateCIDNumberAsync(_db, "IND");

            var customer = new Customer
            {
                EntityType = EntityType.Individual,
                CustomerCategories = request.Individual.CustomerCategories,
                CIDNumber = cidNumber,
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
            _db.IndividualIdentifications.Add(MapIdentification(request.Identification, customerId));
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
                        RelationType = RelationType.Relative,
                        Name = rel.Name,
                        Relationship = rel.Relationship,
                        HighestPositionOccupied = rel.Position,
                        PeriodCovered = rel.PeriodCovered
                    });
                }
            }

            // 10b. Save Gov Official Positions List (PEP — stored in GovernmentRelation with RelationType.Self)
            if (request.Individual.IsGovOfficial && request.Individual.GovOfficialPositions != null)
            {
                foreach (var pos in request.Individual.GovOfficialPositions)
                {
                    _db.GovernmentRelations.Add(new GovernmentRelation
                    {
                        CustomerID = customerId,
                        EntityType = EntityType.Individual,
                        RelationType = RelationType.Self,
                        HighestPositionOccupied = pos.Position,
                        PeriodCovered = pos.Period
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
            var ack = _mapper.Map<ClientAcknowledgement>(request.Acknowledgement);
            ack.CustomerID = customerId;
            ack.EntityType = EntityType.Individual;
            _db.ClientAcknowledgements.Add(ack);

            // 14. Save Bank Review
            var bankReview = _mapper.Map<BankReview>(request.BankReview);
            bankReview.CustomerID = customerId;
            bankReview.EntityType = EntityType.Individual;
            _db.BankReviews.Add(bankReview);

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();

            return (customerId, cidNumber);
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

        if (response.Individual != null && infoModel != null)
        {
            // IsGov in DB → IsGovOfficial in DTO (names differ — AutoMapper won't auto-map)
            response.Individual.IsGovOfficial = infoModel.IsGov;
            // Sync Step 1 & Meta Data from Customer Table
            response.Individual.CustomerCategories = customer.CustomerCategories;
            response.Individual.CIDNumber = customer.CIDNumber;

            // Populate GovOfficialPositions from GovernmentRelation (RelationType.Self = PEP self-positions)
            response.Individual.GovOfficialPositions = await _db.GovernmentRelations
                .Where(x => x.CustomerID == customerId && x.RelationType == RelationType.Self)
                .Select(x => new IndividualInfoDTO.PageModel.GovOfficialPositionModel
                {
                    CustomerID = x.CustomerID,
                    Position = x.HighestPositionOccupied,
                    Period = x.PeriodCovered
                }).ToListAsync();

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
                .Where(x => x.CustomerID == customerId && x.RelationType != RelationType.Self)
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

        // Sync Family fields into Individual so Review_Submit can read them from Personal
        if (family != null && response.Individual != null)
        {
            response.Individual.SpouseGivenName = family.SpouseGivenName;
            response.Individual.SpouseLastName = family.SpouseLastName;
            response.Individual.SpouseMiddleName = family.SpouseMiddleName;
            response.Individual.MotherMaidenGivenName = family.MotherMaidenGivenName;
            response.Individual.MotherMaidenLastName = family.MotherMaidenLastName;
            response.Individual.MotherMaidenMiddleName = family.MotherMaidenMiddleName;
        }

        // 9. Table: IndividualForeigners (Optional)
        var foreigner = await _db.IndividualForeigners.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        response.Foreigner = _mapper.Map<IndividualForeignerDTO.PageModel>(foreigner);

        // 10. Table: Contacts & AccountPurposes (Combined logic)
        var contact = await _db.Contacts.AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntityID == customerId && e.EntityType == EntityType.Individual);
        response.Contact = _mapper.Map<ContactDTO.PageModel>(contact);

        // Sync Contact fields into Individual so Review_Submit can read them from Personal
        if (contact != null && response.Individual != null)
        {
            response.Individual.HomePhoneNumber = contact.HomePhoneNumber;
            response.Individual.MobilePhoneNumber = contact.MobilePhoneNumber;
            response.Individual.EmailAddress = contact.EmailAddress;
        }

        var purpose = await _db.AccountPurposes.AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntityID == customerId && e.EntityType == EntityType.Individual);
        if (purpose != null)
        {
            response.AccountPurpose = _mapper.Map<AccountPurposeDTO.PageModel>(purpose);

            // AccountContact mirrors the individual's account purpose data for the print form
            response.AccountContact = new BusinessInfoDTO.PageModel
            {
                AccountPurpose = purpose.PurposeOfAccount,
                AccountPurposeOther = purpose.PurposeOfAccountOther,
                ProductsAvailed = purpose.ProductsAvailed,
                ProductsAvailedOther = purpose.ProductsAvailedOther,
                OfficePhoneNo = contact?.MobilePhoneNumber,
                EmailAddress = contact?.EmailAddress,
                ContactPerson = contact?.ContactPerson,
            };
        }

        // 11. Table: BusinessInfo (optional — only for customers with business)
        var biz = await _db.BusinessInfos.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        if (biz != null)
            response.Business = _mapper.Map<BusinessInfoDTO.PageModel>(biz);

        // 12. Table: Beneficiaries (optional)
        var beneficiary = await _db.Beneficiaries.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        if (beneficiary != null)
            response.Beneficiary = _mapper.Map<BeneficiaryDTO.PageModel>(beneficiary);

        // 13. Table: ClientAcknowledgements
        var ack = await _db.ClientAcknowledgements.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        if (ack != null)
            response.Acknowledgement = _mapper.Map<ClientAcknowledgementDTO.PageModel>(ack);

        // 14. Table: BankReview
        var bankReview = await _db.BankReviews.AsNoTracking()
            .FirstOrDefaultAsync(e => e.CustomerID == customerId);
        if (bankReview != null)
        {
            var br = new BankReviewDTO.PageModel
            {
                BankReviewID = bankReview.BankReviewID,
                CustomerID = bankReview.CustomerID,
                IsEmployee = bankReview.IsEmployee,
                IsDosri = bankReview.IsDosri,
                IsRpt = bankReview.IsRpt,
                Position = bankReview.Position,
                IsRelative = bankReview.IsRelative,
                RelativeEmployeeName = bankReview.RelativeEmployeeName,
                RelativePosition = bankReview.RelativePosition,
                RelativeRelationship = bankReview.RelativeRelationship,
                IsEntityOwnedByEmployee = bankReview.IsEntityOwnedByEmployee,
                NatureOfWorkBusinessOther = bankReview.NatureOfWorkBusinessOther,
                SelectedWork = !string.IsNullOrEmpty(bankReview.NatureOfWorkBusinessOther) ? "OTHERS" : null,
                IsOwnedByPEP = bankReview.IsOwnedByPEP,
                DocumentsOther = bankReview.DocumentsOther,
                SignatureAuthenticated = bankReview.ReviewerSignature,
                VerifiedBy = bankReview.VerifiedBy,
                ApprovedBy = bankReview.ApprovedBy,
                Remarks = bankReview.Remarks,
                ScreeningList = string.IsNullOrEmpty(bankReview.CheckedAgainst)
                    ? new() : bankReview.CheckedAgainst.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList(),
                WorkList = string.IsNullOrEmpty(bankReview.NatureOfWorkBusiness)
                    ? new() : bankReview.NatureOfWorkBusiness.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList(),
                DocsList = string.IsNullOrEmpty(bankReview.DocumentsPresented)
                    ? new() : bankReview.DocumentsPresented.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList(),
                AdditionalDocsList = string.IsNullOrEmpty(bankReview.AdditionalDocuments)
                    ? new() : bankReview.AdditionalDocuments.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList(),
            };
            response.BankReview = br;
        }

        return response;
    }

    // ── CID Number Generator ──────────────────────────────────────────
    // Format: {prefix}{7-digit sequence} e.g. IND0000001, BUS0000001
    // Queries the last CID with the same prefix and increments by 1.
    // Safe within a transaction — no race condition.
    private static async Task<string> GenerateCIDNumberAsync(
        CISDbContext db, string prefix)
    {
        var last = await db.Customers
            .Where(c => c.CIDNumber != null && c.CIDNumber.StartsWith(prefix))
            .OrderByDescending(c => c.CIDNumber)
            .Select(c => c.CIDNumber)
            .FirstOrDefaultAsync();

        int next = 1;
        if (last != null && last.Length > prefix.Length)
        {
            if (int.TryParse(last[prefix.Length..], out int parsed))
                next = parsed + 1;
        }

        return $"{prefix}{next:D7}";
    }

    // ── Manual mapping for IndividualIdentification ────────────────
    // AutoMapper ReverseMap() cannot invert custom MapFrom expressions,
    // so we parse each DataUrl into bytes + contentType here.
    private static IndividualIdentification MapIdentification(
        IndividualIdentificationDTO.PageModel src, long customerId)
    {
        static (byte[]? b, string? ct) Parse(string? url)
            => IndividualIdentificationDTO.ParseDataUrl(url);

        var (selfieB, selfieCt) = Parse(src.SelfieDataUrl);
        var (tinFB, tinFCt) = Parse(src.TINFrontDataUrl);
        var (tinBB, tinBCt) = Parse(src.TINBackDataUrl);
        var (sssFB, sssFCt) = Parse(src.SSSFrontDataUrl);
        var (sssBB, sssBCt) = Parse(src.SSSBackDataUrl);
        var (gsisFB, gsisFCt) = Parse(src.GSISFrontDataUrl);
        var (gsisBB, gsisBCt) = Parse(src.GSISBackDataUrl);
        var (dlFB, dlFCt) = Parse(src.DriversLicenseFrontDataUrl);
        var (dlBB, dlBCt) = Parse(src.DriversLicenseBackDataUrl);
        var (ppFB, ppFCt) = Parse(src.PassportFrontDataUrl);
        var (ppBB, ppBCt) = Parse(src.PassportBackDataUrl);
        var (othFB, othFCt) = Parse(src.OtherIDFrontDataUrl);
        var (othBB, othBCt) = Parse(src.OtherIDBackDataUrl);

        return new IndividualIdentification
        {
            CustomerID = customerId,
            TINNumber = src.TINNumber,
            SSSNumber = src.SSSNumber,
            GSISNumber = src.GSISNumber,
            DriversLicenseIDNo = src.DriversLicenseIDNo,
            DriversLicenseExpiry = src.DriversLicenseExpiry,
            PassportIDNo = src.PassportIDNo,
            PassportIDExpiry = src.PassportIDExpiry,
            OtherIDType = src.OtherIDType,
            OtherIDNumber = src.OtherIDNumber,
            OtherIDExpiry = src.OtherIDExpiry,

            SelfieImage = selfieB,
            SelfieContentType = selfieCt,
            TINFrontImage = tinFB,
            TINFrontContentType = tinFCt,
            TINBackImage = tinBB,
            TINBackContentType = tinBCt,
            SSSFrontImage = sssFB,
            SSSFrontContentType = sssFCt,
            SSSBackImage = sssBB,
            SSSBackContentType = sssBCt,
            GSISFrontImage = gsisFB,
            GSISFrontContentType = gsisFCt,
            GSISBackImage = gsisBB,
            GSISBackContentType = gsisBCt,
            DriversLicenseFrontImage = dlFB,
            DriversLicenseFrontContentType = dlFCt,
            DriversLicenseBackImage = dlBB,
            DriversLicenseBackContentType = dlBCt,
            PassportFrontImage = ppFB,
            PassportFrontContentType = ppFCt,
            PassportBackImage = ppBB,
            PassportBackContentType = ppBCt,
            OtherIDFrontImage = othFB,
            OtherIDFrontContentType = othFCt,
            OtherIDBackImage = othBB,
            OtherIDBackContentType = othBCt,
        };
    }
}