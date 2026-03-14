using AutoMapper;
using FluentValidation;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class GovernmentRelationDTO
{
    public class Browse
    {
        public long GovernmentRelationID { get; set; }
        public string? Name { get; set; }
        public string? Relationship { get; set; }
    }

    public class PageModel
    {
        public long GovernmentRelationID { get; set; }
        public long CustomerID { get; set; }
        public string? Name { get; set; }
        public string? Relationship { get; set; }
        public string? HighestPositionOccupied { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
                RuleFor(x => x.Relationship).NotEmpty().MaximumLength(100);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GovernmentRelation, Browse>().ReverseMap();
            CreateMap<GovernmentRelation, PageModel>().ReverseMap();
        }
    }
}