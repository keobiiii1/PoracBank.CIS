using CIS.Assets.DTO;
using CIS.Assets.Models;

namespace CIS.API.Repositories;

internal static class DtoMapper
{
    public static Address ToAddress(AddressDTO.PageModel source)
    {
        var target = new Address();
        CopyAddress(source, target);
        return target;
    }

    public static AddressDTO.PageModel ToAddressPageModel(Address? source)
        => source is null
            ? new AddressDTO.PageModel()
            : new AddressDTO.PageModel
            {
                AddressID = source.AddressID,
                EntityID = source.EntityID,
                EntityType = source.EntityType,
                PermanentAddress = source.PermanentAddress,
                PermanentZipCode = source.PermanentZipCode,
                PermanentCountry = source.PermanentCountry,
                PresentAddress = source.PresentAddress,
                PresentZipCode = source.PresentZipCode,
                PresentCountry = source.PresentCountry,
                BusinessAddress = source.BusinessAddress,
                PrincipalAddress = source.PrincipalAddress,
                MailingPreference = source.MailingPreference,
            };

    public static void CopyAddress(AddressDTO.PageModel source, Address target)
    {
        target.AddressID = source.AddressID;
        target.EntityID = source.EntityID;
        target.EntityType = source.EntityType;
        target.PermanentAddress = source.PermanentAddress;
        target.PermanentZipCode = source.PermanentZipCode;
        target.PermanentCountry = source.PermanentCountry;
        target.PresentAddress = source.PresentAddress;
        target.PresentZipCode = source.PresentZipCode;
        target.PresentCountry = source.PresentCountry;
        target.BusinessAddress = source.BusinessAddress;
        target.PrincipalAddress = source.PrincipalAddress;
        target.MailingPreference = source.MailingPreference;
    }

    public static Contact ToContact(ContactDTO.PageModel source)
    {
        var target = new Contact();
        CopyContact(source, target);
        return target;
    }

    public static ContactDTO.PageModel ToContactPageModel(Contact? source)
        => source is null
            ? new ContactDTO.PageModel()
            : new ContactDTO.PageModel
            {
                ContactID = source.ContactID,
                EntityID = source.EntityID,
                EntityType = source.EntityType,
                HomePhoneNumber = source.HomePhoneNumber,
                MobilePhoneNumber = source.MobilePhoneNumber,
                EmailAddress = source.EmailAddress,
                ContactPerson = source.ContactPerson,
            };

    public static void CopyContact(ContactDTO.PageModel source, Contact target)
    {
        target.ContactID = source.ContactID;
        target.EntityID = source.EntityID;
        target.EntityType = source.EntityType;
        target.HomePhoneNumber = source.HomePhoneNumber;
        target.MobilePhoneNumber = source.MobilePhoneNumber;
        target.EmailAddress = source.EmailAddress;
        target.ContactPerson = source.ContactPerson;
    }

    public static IndividualInfo ToIndividualInfo(IndividualInfoDTO.PageModel source)
        => new()
        {
            IndividualInfoID = source.IndividualInfoID,
            CustomerID = source.CustomerID,
            FirstName = source.FirstName,
            MiddleName = source.MiddleName,
            LastName = source.LastName,
            MaidenName = source.MaidenName,
            MaidenMiddleName = source.MaidenMiddleName,
            MaidenLastName = source.MaidenLastName,
            IsResident = source.IsResident,
            IsGov = source.IsGovOfficial,
            GovPosition = source.GovPosition,
            GovPeriod = source.GovPeriod,
            Citizenship = source.Citizenship,
            CitizenshipOther = source.CitizenshipOther,
            CountryOfOrigin = source.CountryOfOrigin,
            DateOfBirth = source.DateOfBirth,
            PlaceOfBirth = source.PlaceOfBirth,
            Gender = source.Gender,
            MaritalStatus = source.MaritalStatus,
        };

    public static IndividualInfoDTO.PageModel ToIndividualInfoPageModel(IndividualInfo? source)
        => source is null
            ? new IndividualInfoDTO.PageModel()
            : new IndividualInfoDTO.PageModel
            {
                IndividualInfoID = source.IndividualInfoID,
                CustomerID = source.CustomerID,
                FirstName = source.FirstName,
                MiddleName = source.MiddleName,
                LastName = source.LastName,
                MaidenName = source.MaidenName,
                MaidenMiddleName = source.MaidenMiddleName,
                MaidenLastName = source.MaidenLastName,
                IsResident = source.IsResident,
                IsGovOfficial = source.IsGov,
                GovPosition = source.GovPosition,
                GovPeriod = source.GovPeriod,
                Citizenship = source.Citizenship,
                CitizenshipOther = source.CitizenshipOther,
                CountryOfOrigin = source.CountryOfOrigin,
                DateOfBirth = source.DateOfBirth,
                PlaceOfBirth = source.PlaceOfBirth,
                Gender = source.Gender,
                MaritalStatus = source.MaritalStatus,
            };

    public static IndividualEmployment ToIndividualEmployment(IndividualEmploymentDTO.PageModel source)
        => new()
        {
            EmploymentID = source.EmploymentID,
            CustomerID = source.CustomerID,
            EmploymentStatus = source.EmploymentStatus,
            EmploymentStatusOther = source.EmploymentStatusOther,
            TypeOfEmployment = source.TypeOfEmployment,
            TypeOfEmploymentOther = source.TypeOfEmploymentOther,
            OFWCountry = source.OFWCountry,
            EducationalAttainment = source.EducationalAttainment,
            NatureOfWork = source.NatureOfWork,
            AverageMonthlyIncome = source.AverageMonthlyIncome,
            SourceOfFunds = source.SourceOfFunds,
            SourceOfFundsOther = source.SourceOfFundsOther,
            NameOfEmployer = source.NameOfEmployer,
            EmployerBuildingNo = source.EmployerBuildingNo,
            EmployerStreet = source.EmployerStreet,
            EmployerBrgyDistrict = source.EmployerBrgyDistrict,
            EmployerCityTown = source.EmployerCityTown,
            EmployerPhoneNumber = source.EmployerPhoneNumber,
            EmployerEmailAddress = source.EmployerEmailAddress,
            PositionRank = source.PositionRank,
        };

    public static IndividualEmploymentDTO.PageModel ToIndividualEmploymentPageModel(IndividualEmployment? source)
        => source is null
            ? new IndividualEmploymentDTO.PageModel()
            : new IndividualEmploymentDTO.PageModel
            {
                EmploymentID = source.EmploymentID,
                CustomerID = source.CustomerID,
                EmploymentStatus = source.EmploymentStatus,
                EmploymentStatusOther = source.EmploymentStatusOther,
                TypeOfEmployment = source.TypeOfEmployment,
                TypeOfEmploymentOther = source.TypeOfEmploymentOther,
                OFWCountry = source.OFWCountry,
                EducationalAttainment = source.EducationalAttainment,
                NatureOfWork = source.NatureOfWork,
                AverageMonthlyIncome = source.AverageMonthlyIncome,
                SourceOfFunds = source.SourceOfFunds,
                SourceOfFundsOther = source.SourceOfFundsOther,
                NameOfEmployer = source.NameOfEmployer,
                EmployerBuildingNo = source.EmployerBuildingNo,
                EmployerStreet = source.EmployerStreet,
                EmployerBrgyDistrict = source.EmployerBrgyDistrict,
                EmployerCityTown = source.EmployerCityTown,
                EmployerPhoneNumber = source.EmployerPhoneNumber,
                EmployerEmailAddress = source.EmployerEmailAddress,
                PositionRank = source.PositionRank,
            };

    public static IndividualFamilyDTO.PageModel ToIndividualFamilyPageModel(IndividualFamily? source)
        => source is null
            ? new IndividualFamilyDTO.PageModel()
            : new IndividualFamilyDTO.PageModel
            {
                FamilyID = source.FamilyID,
                CustomerID = source.CustomerID,
                SpouseLastName = source.SpouseLastName,
                SpouseGivenName = source.SpouseGivenName,
                SpouseMiddleName = source.SpouseMiddleName,
                MotherMaidenLastName = source.MotherMaidenLastName,
                MotherMaidenMiddleName = source.MotherMaidenMiddleName,
                MotherMaidenGivenName = source.MotherMaidenGivenName,
            };

    public static IndividualForeignerDTO.PageModel ToIndividualForeignerPageModel(IndividualForeigner? source)
        => source is null
            ? new IndividualForeignerDTO.PageModel()
            : new IndividualForeignerDTO.PageModel
            {
                ForeignerID = source.ForeignerID,
                CustomerID = source.CustomerID,
                PassportIDNumber = source.PassportIDNumber,
                PassportExpiry = source.PassportExpiry,
                IsACR = source.IsACR,
                IsSIRV = source.IsSIRV,
                IsSRRV = source.IsSRRV,
            };

    public static IndividualIdentificationDTO.PageModel ToIndividualIdentificationPageModel(IndividualIdentification? source)
        => source is null
            ? new IndividualIdentificationDTO.PageModel()
            : new IndividualIdentificationDTO.PageModel
            {
                IdentificationID = source.IdentificationID,
                CustomerID = source.CustomerID,
                TINNumber = source.TINNumber,
                SSSNumber = source.SSSNumber,
                GSISNumber = source.GSISNumber,
                DriversLicenseIDNo = source.DriversLicenseIDNo,
                DriversLicenseExpiry = source.DriversLicenseExpiry,
                PassportIDNo = source.PassportIDNo,
                PassportIDExpiry = source.PassportIDExpiry,
                OtherIDType = source.OtherIDType,
                OtherIDNumber = source.OtherIDNumber,
                OtherIDExpiry = source.OtherIDExpiry,
                SelfieDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.SelfieImage, source.SelfieContentType),
                TINFrontDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.TINFrontImage, source.TINFrontContentType),
                TINBackDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.TINBackImage, source.TINBackContentType),
                SSSFrontDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.SSSFrontImage, source.SSSFrontContentType),
                SSSBackDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.SSSBackImage, source.SSSBackContentType),
                GSISFrontDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.GSISFrontImage, source.GSISFrontContentType),
                GSISBackDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.GSISBackImage, source.GSISBackContentType),
                DriversLicenseFrontDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.DriversLicenseFrontImage, source.DriversLicenseFrontContentType),
                DriversLicenseBackDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.DriversLicenseBackImage, source.DriversLicenseBackContentType),
                PassportFrontDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.PassportFrontImage, source.PassportFrontContentType),
                PassportBackDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.PassportBackImage, source.PassportBackContentType),
                OtherIDFrontDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.OtherIDFrontImage, source.OtherIDFrontContentType),
                OtherIDBackDataUrl = IndividualIdentificationDTO.BuildDataUrl(source.OtherIDBackImage, source.OtherIDBackContentType),
            };

    public static BusinessInfo ToBusinessInfo(BusinessInfoDTO.PageModel source)
        => new()
        {
            BusinessInfoID = source.BusinessInfoID,
            CustomerID = source.CustomerID,
            NameOfBusiness = source.NameOfBusiness,
            IsGovernment = source.IsGovernment,
            IsPrivate = source.IsPrivate,
            TypeOfOrganization = source.TypeOfOrganization,
            TypeOfOrganizationOther = source.TypeOfOrganizationOther,
            DateOfRegistration = source.DateOfRegistration,
            BusinessRegNumber = source.BusinessRegNumber,
            BusinessRegExpiry = source.BusinessRegExpiry,
            PlaceOfRegistration = source.PlaceOfRegistration,
            NatureOfBusiness = source.NatureOfBusiness,
            TaxIdentificationNumber = source.TaxIdentificationNumber,
            DTICertNumber = source.DTICertNumber,
            DTICertExpiry = source.DTICertExpiry,
            SizeOfBusiness = source.SizeOfBusiness,
            AverageAnnualIncome = source.AverageAnnualIncome,
        };

    public static BusinessInfoDTO.PageModel ToBusinessInfoPageModel(BusinessInfo? source)
        => source is null
            ? new BusinessInfoDTO.PageModel()
            : new BusinessInfoDTO.PageModel
            {
                BusinessInfoID = source.BusinessInfoID,
                CustomerID = source.CustomerID,
                NameOfBusiness = source.NameOfBusiness,
                IsGovernment = source.IsGovernment,
                IsPrivate = source.IsPrivate,
                TypeOfOrganization = source.TypeOfOrganization,
                TypeOfOrganizationOther = source.TypeOfOrganizationOther,
                DateOfRegistration = source.DateOfRegistration,
                BusinessRegNumber = source.BusinessRegNumber,
                BusinessRegExpiry = source.BusinessRegExpiry,
                PlaceOfRegistration = source.PlaceOfRegistration,
                NatureOfBusiness = source.NatureOfBusiness,
                TaxIdentificationNumber = source.TaxIdentificationNumber,
                DTICertNumber = source.DTICertNumber,
                DTICertExpiry = source.DTICertExpiry,
                SizeOfBusiness = source.SizeOfBusiness,
                AverageAnnualIncome = source.AverageAnnualIncome,
            };

    public static Beneficiary ToBeneficiary(BeneficiaryDTO.PageModel source)
        => new()
        {
            BeneficiaryID = source.BeneficiaryID,
            CustomerID = source.CustomerID,
            TrustType = source.TrustType,
            BeneficiaryName = source.BeneficiaryName,
            Birthday = source.Birthday,
            PlaceOfBirth = source.PlaceOfBirth,
            Nationality = source.Nationality,
            Address = source.Address,
            NatureOfWork = source.NatureOfWork,
        };

    public static BeneficiaryDTO.PageModel ToBeneficiaryPageModel(Beneficiary? source)
        => source is null
            ? new BeneficiaryDTO.PageModel()
            : new BeneficiaryDTO.PageModel
            {
                BeneficiaryID = source.BeneficiaryID,
                CustomerID = source.CustomerID,
                TrustType = source.TrustType,
                BeneficiaryName = source.BeneficiaryName,
                Birthday = source.Birthday,
                PlaceOfBirth = source.PlaceOfBirth,
                Nationality = source.Nationality,
                Address = source.Address,
                NatureOfWork = source.NatureOfWork,
            };

    public static ClientAcknowledgement ToClientAcknowledgement(ClientAcknowledgementDTO.PageModel source)
        => new()
        {
            ClientAcknowledgementID = source.ClientAcknowledgementID,
            CustomerID = source.CustomerID,
            SignatureData = source.SignatureData,
            PrintedName = source.PrintedName,
            DateSigned = source.DateSigned,
        };

    public static ClientAcknowledgementDTO.PageModel ToClientAcknowledgementPageModel(ClientAcknowledgement? source)
        => source is null
            ? new ClientAcknowledgementDTO.PageModel()
            : new ClientAcknowledgementDTO.PageModel
            {
                ClientAcknowledgementID = source.ClientAcknowledgementID,
                CustomerID = source.CustomerID,
                SignatureData = source.SignatureData,
                PrintedName = source.PrintedName,
                DateSigned = source.DateSigned,
            };

    public static BankReview ToBankReview(BankReviewDTO.PageModel source)
    {
        var target = new BankReview();
        CopyBankReview(source, target);
        return target;
    }

    public static void CopyBankReview(BankReviewDTO.PageModel source, BankReview target)
    {
        target.BankReviewID = source.BankReviewID;
        target.CustomerID = source.CustomerID;
        target.CheckedAgainst = JoinList(source.ScreeningList);
        target.NatureOfWorkBusiness = JoinList(source.WorkList);
        target.DocumentsPresented = JoinList(source.DocsList);
        target.AdditionalDocuments = JoinList(source.AdditionalDocsList);
        target.IsEmployee = source.IsEmployee;
        target.IsDosri = source.IsDosri;
        target.IsRpt = source.IsRpt;
        target.Position = source.Position;
        target.IsRelative = source.IsRelative;
        target.RelativeEmployeeName = source.RelativeEmployeeName;
        target.RelativePosition = source.RelativePosition;
        target.RelativeRelationship = source.RelativeRelationship;
        target.IsEntityOwnedByEmployee = source.IsEntityOwnedByEmployee;
        target.NatureOfWorkBusinessOther = source.NatureOfWorkBusinessOther;
        target.IsOwnedByPEP = source.IsOwnedByPEP;
        target.DocumentsOther = source.DocumentsOther;
        target.SignatureAuthenticated = source.SignatureAuthenticated;
        target.ReviewerSignature = source.SignatureAuthenticated;
        target.VerifiedBy = source.VerifiedBy;
        target.ApprovedBy = source.ApprovedBy;
        target.Remarks = source.Remarks;
    }

    public static AccountPurposeDTO.PageModel ToAccountPurposePageModel(AccountPurpose? source)
        => source is null
            ? new AccountPurposeDTO.PageModel()
            : new AccountPurposeDTO.PageModel
            {
                AccountPurposeID = source.AccountPurposeID,
                EntityID = source.EntityID,
                EntityType = source.EntityType,
                PurposeOfAccount = source.PurposeOfAccount,
                PurposeOfAccountOther = source.PurposeOfAccountOther,
                ProductsAvailed = source.ProductsAvailed,
                ProductsAvailedOther = source.ProductsAvailedOther,
            };

    private static string? JoinList(IEnumerable<string>? values)
        => values is null
            ? null
            : string.Join(",", values.Select(x => x.Trim()).Where(x => x.Length > 0));
}
