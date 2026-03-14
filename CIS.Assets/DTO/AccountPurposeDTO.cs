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
        public PurposeOfAccount PurposeOfAccount { get; set; }
        public bool ProductSavings { get; set; }
        public bool ProductCurrent { get; set; }

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
            CreateMap<AccountPurpose, Browse>().ReverseMap();
            CreateMap<AccountPurpose, PageModel>().ReverseMap();
        }
    }
}