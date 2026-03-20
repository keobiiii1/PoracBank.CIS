using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("Customer")]
public class Customer
{
    public long CustomerID { get; set; }
    public EntityType EntityType { get; set; }
    public List<CustomerCategory> CustomerCategories { get; set; } = new();
    public string? CIDNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public IndividualInfo? IndividualInfo { get; set; }
    public BusinessInfo? BusinessInfo { get; set; }
    public Beneficiary? Beneficiary { get; set; }
    public Address? Address { get; set; }
    public Contact? Contact { get; set; }
    public AccountPurpose? AccountPurpose { get; set; }
    public SourceOfFunds? SourceOfFunds { get; set; }
    public ICollection<BusinessInterest> BusinessInterests { get; set; } = new List<BusinessInterest>();
    public ICollection<GovernmentRelation> GovernmentRelations { get; set; } = new List<GovernmentRelation>();
    public BankReview? BankReview { get; set; }
    public ClientAcknowledgement? ClientAcknowledgement { get; set; }
}