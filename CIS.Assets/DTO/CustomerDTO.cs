using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CIS.Assets.DTO;

public class CustomerDTO
{
    public class Browse
    {
        public long CustomerID { get; set; }
        public string? CIDNumber { get; set; }
        public EntityType EntityType { get; set; }
        public CustomerCategory CustomerCategory { get; set; }
        public CustomerType CustomerType { get; set; }
    }

    public class Filter
    {
        public string? SearchText { get; set; }
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
    }

    public class PageModel
    {
        public long CustomerID { get; set; }
        public EntityType EntityType { get; set; }
        public CustomerCategory CustomerCategory { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? CIDNumber { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.CustomerCategory).NotEqual(CustomerCategory.None).WithMessage("Category is required.");
                RuleFor(x => x.CustomerType).NotEqual(CustomerType.None).WithMessage("Type is required.");
                RuleFor(x => x.CIDNumber).MaximumLength(50);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, Browse>().ReverseMap();
            CreateMap<Customer, PageModel>().ReverseMap();
        }
    }
}