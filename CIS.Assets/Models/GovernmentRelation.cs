using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("GovernmentRelation")]
public class GovernmentRelation 
{
    public long GovernmentRelationID { get; set; }
    public long CustomerID { get; set; }
    public RelationType RelationType { get; set; } = RelationType.None;
    public string? Name { get; set; }
    public string? Relationship { get; set; }
    public string? HighestPositionOccupied { get; set; }
    public string? PeriodCovered { get; set; }
    public Customer Customer { get; set; } = null!;
}