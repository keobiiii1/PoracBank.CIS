using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("BusinessInterest")]
public class BusinessInterest
{
    public long BusinessInterestID { get; set; }
    public long CustomerID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.Individual;
    public string? BusinessName { get; set; }
    public decimal? OwnershipPercentage { get; set; }
    public Customer Customer { get; set; } = null!;
}