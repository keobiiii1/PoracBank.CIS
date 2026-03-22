using AutoMapper;
using System.ComponentModel.DataAnnotations;
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
        public List<CustomerCategory> CustomerCategories { get; set; } = new();

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string? FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Middle name cannot exceed 100 characters.")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string? LastName { get; set; }

        public bool IsResident { get; set; } = true;
        [Range(1, int.MaxValue, ErrorMessage = "Citizenship is required.")]
        public Citizenship Citizenship { get; set; }
        public string? CitizenshipOther { get; set; }
        [Required(ErrorMessage = "Country of origin is required.")]
        [MaxLength(100)]
        public string? CountryOfOrigin { get; set; }
        [Required(ErrorMessage = "Date of birth is required.")]
        [DateOfBirthValidation]
        public DateOnly? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        // Female + Married maiden name
        public string? MaidenName { get; set; }
        public string? MaidenMiddleName { get; set; }
        [MaxLength(100)]
        public string? MaidenLastName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Marital status is required.")]
        public MaritalStatus MaritalStatus { get; set; }

        // Spouse Details
        [MaxLength(100)]
        public string? SpouseGivenName { get; set; }
        public string? SpouseMiddleName { get; set; }
        [MaxLength(100)]
        public string? SpouseLastName { get; set; }

        // Mother's Maiden Name
        [Required(ErrorMessage = "Mother's given name is required.")]
        [MaxLength(100)]
        public string? MotherMaidenGivenName { get; set; }
        [Required(ErrorMessage = "Mother's middle name is required.")]
        [MaxLength(100)]
        public string? MotherMaidenMiddleName { get; set; }
        [Required(ErrorMessage = "Mother's last name is required.")]
        [MaxLength(100)]
        public string? MotherMaidenLastName { get; set; }

        // Foreigner
        public bool IsForeigner { get; set; }
        [MaxLength(50)]
        public string? PassportIDNumber { get; set; }
        public DateOnly? PassportExpiry { get; set; }
        public bool IsACR { get; set; }
        public bool IsSIRV { get; set; }
        public bool IsSRRV { get; set; }

        public string? HomePhoneNumber { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }

        public PurposeOfAccount AccountPurpose { get; set; }
        public string? AccountPurposeOther { get; set; }
        public string? ProductsAvailed { get; set; }
        public string? ProductsAvailedOther { get; set; }

        // PEP
        public bool IsGovOfficial { get; set; }
        public string? GovPosition { get; set; }
        public string? GovPeriod { get; set; }
        public bool HasGovRelative { get; set; }

        public List<BusinessInterestModel> BusinessInterests { get; set; } = new();
        public List<GovRelativeModel> GovRelatives { get; set; } = new();
        public List<GovOfficialPositionModel> GovOfficialPositions { get; set; } = new();


        public class BusinessInterestModel
        {
            public long CustomerID { get; set; }
            public string? BusinessName { get; set; }
            public string? Relationship { get; set; }
            public decimal? OwnershipPercentage { get; set; }
            public string? PeriodCovered { get; set; }
        }

        public class GovRelativeModel
        {
            public long CustomerID { get; set; }
            public string? Name { get; set; }
            public string? Relationship { get; set; }
            public string? Position { get; set; }
            public string? PeriodCovered { get; set; }
        }

        public class GovOfficialPositionModel
        {
            public long CustomerID { get; set; }
            public string? Position { get; set; }
            public string? Period { get; set; }
        }
    }

    public class DateOfBirthValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is not DateOnly date)
                return ValidationResult.Success;

            var today = DateOnly.FromDateTime(DateTime.Today);

            if (date > today)
                return new ValidationResult("Date of birth cannot be a future date.");

            if (date > today.AddYears(-21))
                return new ValidationResult("Applicant must be at least 21 years old.");

            return ValidationResult.Success;
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