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

        public MailingPreference MailingPreference { get; set; }

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
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualInfo, Browse>().ReverseMap();
            CreateMap<IndividualInfo, PageModel>().ReverseMap();
            CreateMap<PageModel, IndividualForeigner>();
        }
    }
}