using AutoMapper;
using FluentValidation;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualFamilyDTO
{
    public class Browse
    {
        public long FamilyID { get; set; }
        public string? SpouseLastName { get; set; }
        public string? MotherMaidenLastName { get; set; }
    }

    public class PageModel
    {
        public long FamilyID { get; set; }
        public long CustomerID { get; set; }
        public string? SpouseLastName { get; set; }
        public string? SpouseGivenName { get; set; }
        public string? MotherMaidenLastName { get; set; }
        public string? MotherMaidenGivenName { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.MotherMaidenLastName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.MotherMaidenGivenName).NotEmpty().MaximumLength(100);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualFamily, Browse>().ReverseMap();
            CreateMap<IndividualFamily, PageModel>().ReverseMap();
        }
    }
}