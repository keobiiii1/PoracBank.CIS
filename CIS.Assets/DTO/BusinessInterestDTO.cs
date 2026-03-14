using AutoMapper;
using FluentValidation;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class BusinessInterestDTO
{
    public class Browse
    {
        public long BusinessInterestID { get; set; }
        public string? BusinessName { get; set; }
        public decimal? OwnershipPercentage { get; set; }
    }

    public class PageModel
    {
        public long BusinessInterestID { get; set; }
        public long CustomerID { get; set; }
        public string? BusinessName { get; set; }
        public decimal? OwnershipPercentage { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.BusinessName).NotEmpty().MaximumLength(200);
                RuleFor(x => x.OwnershipPercentage).InclusiveBetween(0, 100);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessInterest, Browse>().ReverseMap();
            CreateMap<BusinessInterest, PageModel>().ReverseMap();
        }
    }
}