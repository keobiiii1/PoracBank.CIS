using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class BeneficiaryDTO
{
    public class Browse
    {
        public long BeneficiaryID { get; set; }
        public string? BeneficiaryName { get; set; }
    }

    public class PageModel
    {
        public long BeneficiaryID { get; set; }
        public long CustomerID { get; set; }
        public TrustType TrustType { get; set; }
        public string? BeneficiaryName { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Nationality { get; set; }
        public string? SourceOfFunds { get; set; }
        public string? SourceOfFundsOther { get; set; }
        public string? NatureOfWork { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Beneficiary, Browse>().ReverseMap();

            CreateMap<PageModel, Beneficiary>()
                .ForMember(dest => dest.BeneficiaryID, opt => opt.Ignore());

            CreateMap<Beneficiary, PageModel>();
        }
    }
}