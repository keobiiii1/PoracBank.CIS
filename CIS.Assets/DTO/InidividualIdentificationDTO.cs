using AutoMapper;
using System.ComponentModel.DataAnnotations;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualIdentificationDTO
{
    public class Browse
    {
        public long IdentificationID { get; set; }
        public string? TINNumber { get; set; }
        public string? SSSNumber { get; set; }
    }

    public class PageModel
    {
        public long IdentificationID { get; set; }
        public long CustomerID { get; set; }

        // Government IDs — at least one required (validated via ValidationMessageStore)
        [MaxLength(50)]
        public string? TINNumber { get; set; }

        [MaxLength(50)]
        public string? SSSNumber { get; set; }

        [MaxLength(50)]
        public string? GSISNumber { get; set; }

        // Driver's License — conditionally required when shown
        [MaxLength(50)]
        public string? DriversLicenseIDNo { get; set; }

        public DateOnly? DriversLicenseExpiry { get; set; }

        // Passport — conditionally required when shown
        [MaxLength(50)]
        public string? PassportIDNo { get; set; }

        public DateOnly? PassportIDExpiry { get; set; }

        // Other ID — conditionally required when shown
        [MaxLength(100)]
        public string? OtherIDType { get; set; }

        [MaxLength(100)]
        public string? OtherIDNumber { get; set; }

        public DateOnly? OtherIDExpiry { get; set; }

        // KYC Images — Government IDs
        public string? TINFrontImagePath { get; set; }
        public string? TINBackImagePath { get; set; }
        public string? SSSFrontImagePath { get; set; }
        public string? SSSBackImagePath { get; set; }
        public string? GSISFrontImagePath { get; set; }
        public string? GSISBackImagePath { get; set; }

        // KYC Images
        public string? SelfieImagePath { get; set; }
        public string? DriversLicenseFrontImagePath { get; set; }
        public string? DriversLicenseBackImagePath { get; set; }
        public string? PassportFrontImagePath { get; set; }
        public string? PassportBackImagePath { get; set; }
        public string? OtherIDFrontImagePath { get; set; }
        public string? OtherIDBackImagePath { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualIdentification, Browse>().ReverseMap();
            CreateMap<IndividualIdentification, IndividualIdentificationDTO.PageModel>().ReverseMap()
                .ForMember(dest => dest.PassportIDExpiry, opt => opt.MapFrom(src => src.PassportIDExpiry));
        }
    }
}