using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualInfoDTO
{
    public class Browse
    {
        public long IndividualInfoID { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class PageModel
    {
        public long IndividualInfoID { get; set; }
        public long CustomerID { get; set; }
        public string? CIDNumber { get; set; }
        public EntityType EntityType { get; set; } = EntityType.Individual;
        public CustomerCategory CustomerCategory { get; set; } = CustomerCategory.None;
        public CustomerType CustomerType { get; set; } = CustomerType.None;
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public bool IsResident { get; set; }
        public Citizenship Citizenship { get; set; }
        public string? CitizenshipOther { get; set; }
        public string? CountryOfOrigin { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public Gender Gender { get; set; }

        // --- Added for Female Maiden Name logic ---
        public string? MaidenFirstName { get; set; }
        public string? MaidenMiddleName { get; set; }
        public string? MaidenLastName { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
        // --- Spouse Details (Separated) ---
        public string? SpouseGivenName { get; set; }
        public string? SpouseMiddleName { get; set; }
        public string? SpouseLastName { get; set; }

        // --- Mother's Details (Separated) ---
        public string? MotherMaidenGivenName { get; set; }
        public string? MotherMaidenMiddleName { get; set; }
        public string? MotherMaidenLastName { get; set; }

        // --- Added for Foreigner logic ---
        public bool IsForeigner { get; set; }
        public string? PassportIDNumber { get; set; }
        public DateOnly? PassportExpiry { get; set; }
        public bool IsACR { get; set; }
        public bool IsSIRV { get; set; }
        public bool IsSRRV { get; set; }

        public string? HomePhoneNumber { get; set; } // Added
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public MailingPreference MailingPreference { get; set; }

        public string? TinNo { get; set; }
        public string? SssNo { get; set; }
        public string? GsisNo { get; set; }

        public PurposeOfAccount AccountPurpose { get; set; }
        public string? AccountPurposeOther { get; set; }
        public string? ProductsAvailed { get; set; }
        public string? ProductsAvailedOther { get; set; }

        // Business Interest
        public string? BusinessName { get; set; }
        public decimal? OwnershipPercentage { get; set; }

        // PEP (Government Official) Details
        public bool IsGovOfficial { get; set; }
        public string? GovPosition { get; set; }
        public string? GovPeriod { get; set; }

        // Relative of Gov Official
        public bool HasGovRelative { get; set; }
        public string? GovRelativeName { get; set; }
        public string? GovRelativeRelationship { get; set; }
        public string? GovRelativePosition { get; set; }
        public string? GovRelativePeriod { get; set; }

        // Business Interest List
        public List<BusinessInterestModel> BusinessInterests { get; set; } = new();
        public List<GovRelativeModel> GovRelatives { get; set; } = new();

        public List<string> SelectedProducts { get; set; } = new();

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.DateOfBirth).NotEmpty();

                // Example of conditional validation
                RuleFor(x => x.MaidenLastName).NotEmpty()
                    .When(x => x.Gender == Gender.Female)
                    .WithMessage("Maiden Name is required for female applicants.");
            }
        }
        public class BusinessInterestModel
        {
            public string? BusinessName { get; set; }
            public string? Relationship { get; set; }
            public decimal? OwnershipPercentage { get; set; }
            public string? PeriodCovered { get; set; }
        }

        public class GovRelativeModel
        {
            public string? Name { get; set; }
            public string? Relationship { get; set; }
            public string? Position { get; set; }
            public string? PeriodCovered { get; set; }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualInfo, Browse>().ReverseMap();

            CreateMap<PageModel, IndividualFamily>().ReverseMap()
            .ForMember(dest => dest.IndividualInfoID, opt => opt.Ignore());

            CreateMap<PageModel, IndividualForeigner>().ReverseMap()
            .ForMember(dest => dest.IndividualInfoID, opt => opt.Ignore());

            CreateMap<IndividualInfo, PageModel>().ReverseMap()
            .ForMember(dest => dest.IndividualInfoID, opt => opt.Ignore());
            CreateMap<PageModel, IndividualForeigner>();
        }
    }
}