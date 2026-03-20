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
        public DateTime DateSigned { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientAcknowledgement, PageModel>().ReverseMap()
                .ForMember(dest => dest.ClientAcknowledgementID, opt => opt.Ignore());
        }
    }
}