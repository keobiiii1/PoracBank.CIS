using AutoMapper;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class ClientAcknowlegdementDTO
{
    public class PageModel
    {
        public long ClientAcknowlegdementID { get; set; }
        public long CustomerID { get; set; }
        public string? SignatureData { get; set; }
        public string? PrintedName { get; set; }
        public DateTime DateSigned { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientAcknowlegdement, PageModel>().ReverseMap()
                .ForMember(dest => dest.ClientAcknowlegdementID, opt => opt.Ignore());
        }
    }
}