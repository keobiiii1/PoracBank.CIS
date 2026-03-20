using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class AccountPurposeDTO
{
    public class Browse
    {
        public long AccountPurposeID { get; set; }
        public PurposeOfAccount PurposeOfAccount { get; set; }
    }

    public class PageModel
    {
        public long AccountPurposeID { get; set; }
        public long EntityID { get; set; }
        public EntityType EntityType { get; set; }
        public PurposeOfAccount PurposeOfAccount { get; set; } = PurposeOfAccount.None;
        public string? PurposeOfAccountOther { get; set; }
        public string? ProductsAvailed { get; set; }
        public string? ProductsAvailedOther { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.PurposeOfAccount).NotEqual(PurposeOfAccount.None);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountPurpose, PageModel>().ReverseMap();

            CreateMap<BusinessInfoDTO.PageModel, AccountPurpose>()
                .ForMember(d => d.AccountPurposeID, o => o.Ignore())
                .ForMember(d => d.EntityID, o => o.MapFrom(s => s.CustomerID))
                .ForMember(d => d.EntityType, o => o.MapFrom(s => EntityType.Business))
                .ForMember(d => d.PurposeOfAccount, o => o.MapFrom(s => s.AccountPurpose))
                .ForMember(d => d.PurposeOfAccountOther, o => o.MapFrom(s => s.AccountPurposeOther));
        }
    }
}