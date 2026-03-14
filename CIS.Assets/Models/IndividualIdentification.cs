using CIS.Assets;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("IndividualIdentification")]
public class IndividualIdentification 
{
    public long IdentificationID { get; set; }
    public long CustomerID { get; set; }
    public string? TINNumber { get; set; }
    public string? SSSNumber { get; set; }
    public string? GSISNumber { get; set; }
    public string? DriversLicenseIDNo { get; set; }
    public DateOnly? DriversLicenseExpiry { get; set; }
    public string? PassportIDNo { get; set; }
    public DateOnly? PassportIDExpiry { get; set; }
    public string? OtherIDType { get; set; }
    public string? OtherIDNumber { get; set; }
    public DateOnly? OtherIDExpiry { get; set; }
    public Customer Customer { get; set; } = null!;
}