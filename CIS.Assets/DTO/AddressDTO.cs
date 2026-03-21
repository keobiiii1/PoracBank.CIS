using AutoMapper;
using System.ComponentModel.DataAnnotations;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class AddressDTO
{
    public class Browse
    {
        public long AddressID { get; set; }
        public string? PermanentAddress { get; set; }
    }

    public class PageModel
    {
        public long AddressID { get; set; }
        public long EntityID { get; set; }
        public EntityType EntityType { get; set; }

        [Required(ErrorMessage = "Permanent address is required.")]
        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string? PermanentAddress { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        [MaxLength(20, ErrorMessage = "Zip code cannot exceed 20 characters.")]
        public string? PermanentZipCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(100)]
        public string? PermanentCountry { get; set; }

        // Present Address — conditionally required (handled via ValidationMessageStore)
        [MaxLength(500)]
        public string? PresentAddress { get; set; }

        [MaxLength(20)]
        public string? PresentZipCode { get; set; }

        [MaxLength(100)]
        public string? PresentCountry { get; set; }

        // Business Fields
        public string? BusinessAddress { get; set; }
        public string? PrincipalAddress { get; set; }

        public bool IsPresentSameAsPermanent { get; set; }

        public MailingPreference MailingPreference { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, Browse>().ReverseMap();
            CreateMap<Address, PageModel>().ReverseMap();
        }
    }
}