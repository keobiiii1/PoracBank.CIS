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
        public EntityType EntityType { get; set; } = EntityType.Business;
        public string? NameOfBusiness { get; set; }
        public bool IsGovernment { get; set; }
        public bool IsPrivate { get; set; }
        public TypeOfOrganization TypeOfOrganization { get; set; }
        public string? TypeOfOrganizationOther { get; set; }
        public DateOnly? DateOfRegistration { get; set; }
        public string? BusinessRegNumber { get; set; }
        public DateOnly? BusinessRegExpiry { get; set; }
        public string? PlaceOfRegistration { get; set; }
        public string? NatureOfBusiness { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? DTICertNumber { get; set; }
        public DateOnly? DTICertExpiry { get; set; }
        public SizeOfBusiness SizeOfBusiness { get; set; }
        public AverageAnnualIncome AverageAnnualIncome { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.NameOfBusiness).NotEmpty().MaximumLength(255);
                RuleFor(x => x.TaxIdentificationNumber).MaximumLength(50);
            }
        }
    }

    public class BusinessSaveRequest
    {
        public PageModel Business { get; set; } = new();
        public AddressDTO.PageModel Address { get; set; } = new();
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