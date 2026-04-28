using System.ComponentModel.DataAnnotations;
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

        [MaxLength(50)]
        public string? HomePhoneNumber { get; set; }

        [MaxLength(50)]
        public string? MobilePhoneNumber { get; set; }

        [MaxLength(200)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? EmailAddress { get; set; }

        [MaxLength(200)]
        public string? ContactPerson { get; set; }
    }
}