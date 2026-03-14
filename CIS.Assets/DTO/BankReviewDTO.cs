using AutoMapper;
using FluentValidation;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class BankReviewDTO
{
    public class Browse
    {
        public long BankReviewID { get; set; }
        public bool IsNegativeListed { get; set; }
        public bool IsPEPListed { get; set; }
    }

    public class PageModel
    {
        public long BankReviewID { get; set; }
        public long CustomerID { get; set; }
        public bool IsNegativeListed { get; set; }
        public bool IsPEPListed { get; set; }
        public string? Remarks { get; set; }
        public string? ApprovedBy { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.ApprovedBy).MaximumLength(200);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BankReview, Browse>().ReverseMap();
            CreateMap<BankReview, PageModel>().ReverseMap();
        }
    }
}