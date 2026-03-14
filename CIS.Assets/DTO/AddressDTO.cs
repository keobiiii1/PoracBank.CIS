using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class AddressDTO
{
    public class Browse
    {
        public long AddressID { get; set; }
        public string? PermanentAddress { get; set; }
    }

    public class PageModel
    {
        public long AddressID { get; set; }
        public long EntityID { get; set; }
        public EntityType EntityType { get; set; }

        // Permanent Address Fields
        public string? PermanentAddress { get; set; }
        public string? PermanentZipCode { get; set; }
        public string? PermanentCountry { get; set; } // Added

        // Present Address Fields
        public string? PresentAddress { get; set; }
        public string? PresentZipCode { get; set; }
        public string? PresentCountry { get; set; } // Added

        public bool IsPresentSameAsPermanent { get; set; } // For the checkbox logic

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.PermanentAddress).NotEmpty().MaximumLength(500);
                RuleFor(x => x.PermanentZipCode).NotEmpty().MaximumLength(20);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, Browse>().ReverseMap();
            CreateMap<Address, PageModel>().ReverseMap();
        }
    }
}