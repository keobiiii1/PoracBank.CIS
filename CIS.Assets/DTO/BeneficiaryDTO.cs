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
        public string? BeneficiaryName { get; set; }
        public TrustType TrustType { get; set; }
        public string? Address { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.BeneficiaryName).NotEmpty().MaximumLength(200);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Beneficiary, Browse>().ReverseMap();
            CreateMap<Beneficiary, PageModel>().ReverseMap();
        }
    }
}