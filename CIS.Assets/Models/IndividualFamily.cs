using CIS.Assets;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("IndividualFamily")]
public class IndividualFamily 
{
    public long FamilyID { get; set; }
    public long CustomerID { get; set; }
    public string? SpouseLastName { get; set; }
    public string? SpouseGivenName { get; set; }
    public string? SpouseMiddleName { get; set; }
    public string? MotherMaidenLastName { get; set; }
    public string? MotherMaidenGivenName { get; set; }
    public string? MotherMaidenMiddleName { get; set; }
    public Customer Customer { get; set; } = null!;
}