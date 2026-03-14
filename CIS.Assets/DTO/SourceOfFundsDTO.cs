using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;

namespace CIS.Assets.DTO;

public class SourceOfFundsDTO
{
    public class Browse
    {
        public long SourceOfFundsID { get; set; }
        public SourceOfFunds SourceOfFundsType { get; set; }
    }

    public class PageModel
    {
        public long SourceOfFundsID { get; set; }
        public long EntityID { get; set; }
        public SourceOfFunds SourceOfFundsType { get; set; }
        public string? SourceOfFundsOther { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.SourceOfFundsType).NotEqual(SourceOfFunds.None);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SourceOfFunds, Browse>().ReverseMap();
            CreateMap<SourceOfFunds, PageModel>().ReverseMap();
        }
    }
}