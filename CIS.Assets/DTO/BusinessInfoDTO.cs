using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class BusinessInfoDTO
{
    public class Browse
    {
        public long BusinessInfoID { get; set; }
        public string? NameOfBusiness { get; set; }
        public string? BusinessRegNumber { get; set; }
    }

    public class PageModel
    {
        public long BusinessInfoID { get; set; }
        public long CustomerID { get; set; }
        public string? NameOfBusiness { get; set; }
        public TypeOfOrganization TypeOfOrganization { get; set; }
        public string? BusinessAddress { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public AverageAnnualIncome AverageAnnualIncome { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.NameOfBusiness).NotEmpty().MaximumLength(200);
                RuleFor(x => x.TaxIdentificationNumber).MaximumLength(20);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessInfo, Browse>().ReverseMap();
            CreateMap<BusinessInfo, PageModel>().ReverseMap();
        }
    }
}