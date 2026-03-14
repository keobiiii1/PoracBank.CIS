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
        public string? TINNumber { get; set; }
        public string? SSSNumber { get; set; }
        public string? GSISNumber { get; set; }
        public string? PassportIDNo { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.TINNumber).MaximumLength(20);
                RuleFor(x => x.SSSNumber).MaximumLength(20);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualIdentification, Browse>().ReverseMap();
            CreateMap<IndividualIdentification, PageModel>().ReverseMap();
        }
    }
}