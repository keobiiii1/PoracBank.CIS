using AutoMapper;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class ClientAcknowledgementDTO
{
    public class PageModel
    {
        public long ClientAcknowledgementID { get; set; }
        public long CustomerID { get; set; }
        public string? SignatureData { get; set; }
        public string? PrintedName { get; set; }
        public DateOnly? DateSigned { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientAcknowledgement, PageModel>()
                .ForMember(dest => dest.DateSigned, opt => opt.MapFrom(src => (DateOnly?)src.DateSigned))
                .ReverseMap()
                .ForMember(dest => dest.ClientAcknowledgementID, opt => opt.Ignore())
                .ForMember(dest => dest.DateSigned, opt => opt.MapFrom(src => src.DateSigned));
        }
    }
}