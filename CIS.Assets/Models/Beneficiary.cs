using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("Beneficiary")]
public class Beneficiary
{
    public long BeneficiaryID { get; set; }
    public long CustomerID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.Individual;
    public TrustType TrustType { get; set; } = TrustType.None;
    public string? BeneficiaryName { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? Address { get; set; }
    public string? NatureOfWork { get; set; }
    public Customer Customer { get; set; } = null!;
}