using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("Contact")]
public class Contact 
{
    public long ContactID { get; set; }
    public long EntityID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.None;
    public string? HomePhoneNumber { get; set; }
    public string? MobilePhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? ContactPerson { get; set; }
}