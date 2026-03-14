using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualEmploymentDTO
{
    public class Browse
    {
        public long EmploymentID { get; set; }
        public string? NameOfEmployer { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
    }

    public class PageModel
    {
        public long EmploymentID { get; set; }
        public long CustomerID { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public TypeOfEmployment TypeOfEmployment { get; set; }
        public string? NameOfEmployer { get; set; }
        public string? NatureOfWork { get; set; }
        public AverageMonthlyIncome AverageMonthlyIncome { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.NameOfEmployer).NotEmpty().MaximumLength(200);
                RuleFor(x => x.NatureOfWork).MaximumLength(200);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualEmployment, Browse>().ReverseMap();
            CreateMap<IndividualEmployment, PageModel>().ReverseMap();
        }
    }
}