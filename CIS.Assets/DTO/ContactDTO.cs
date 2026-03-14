using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class ContactDTO
{
    public class Browse
    {
        public long ContactID { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
    }

    public class PageModel
    {
        public long ContactID { get; set; }
        public long EntityID { get; set; }
        public EntityType EntityType { get; set; }
        public string? HomePhoneNumber { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactPerson { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.MobilePhoneNumber).NotEmpty().MaximumLength(50);
                RuleFor(x => x.EmailAddress).EmailAddress().MaximumLength(200);
            }
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, Browse>().ReverseMap();
            CreateMap<Contact, PageModel>().ReverseMap();
        }
    }
}