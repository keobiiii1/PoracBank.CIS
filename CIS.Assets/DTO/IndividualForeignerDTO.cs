using AutoMapper;
using FluentValidation;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualForeignerDTO
{
    public class Browse
    {
        public long ForeignerID { get; set; }
        public string? PassportIDNumber { get; set; }
        public bool IsACR { get; set; }
    }

    public class PageModel
    {
        public long ForeignerID { get; set; }
        public long CustomerID { get; set; }
        public string? PassportIDNumber { get; set; }
        public bool IsACR { get; set; }
        public bool IsSIRV { get; set; }
        public bool IsSRRV { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.PassportIDNumber).MaximumLength(50);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualForeigner, Browse>().ReverseMap();

            CreateMap<PageModel, IndividualForeigner>().ReverseMap();

            CreateMap<IndividualInfoDTO.PageModel, IndividualForeigner>()
                .ForMember(dest => dest.ForeignerID, opt => opt.Ignore());
        }
    }
}