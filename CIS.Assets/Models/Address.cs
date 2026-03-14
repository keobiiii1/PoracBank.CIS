using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("Address")]
public class Address 
{
    public long AddressID { get; set; }
    public long EntityID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.None;
    public string? PermanentAddress { get; set; }
    public string? PermanentZipCode { get; set; }
    public string? PermanentCountry { get; set; }
    public string? PresentAddress { get; set; }
    public string? PresentZipCode { get; set; }
    public string? PresentCountry { get; set; }
}