using AutoMapper;
using FluentValidation;
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

        // Personal Identification
        public string? TINNumber { get; set; }
        public string? SSSNumber { get; set; }
        public string? GSISNumber { get; set; }

        // Driver's License
        public string? DriversLicenseIDNo { get; set; }
        public DateOnly? DriversLicenseExpiry { get; set; }

        // Passport
        public string? PassportIDNo { get; set; }
        public DateOnly? PassportIDExpiry { get; set; }

        // Others
        public string? OtherIDType { get; set; }
        public string? OtherIDNumber { get; set; }
        public DateOnly? OtherIDExpiry { get; set; }
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