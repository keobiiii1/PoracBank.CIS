using CIS.Assets;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("IndividualForeigner")]
public class IndividualForeigner 
{
    public long ForeignerID { get; set; }
    public long CustomerID { get; set; }
    public string? PassportIDNumber { get; set; }
    public DateOnly? PassportExpiry { get; set; }
    public bool IsACR { get; set; }
    public bool IsSIRV { get; set; }
    public bool IsSRRV { get; set; }
    public Customer Customer { get; set; } = null!;
}