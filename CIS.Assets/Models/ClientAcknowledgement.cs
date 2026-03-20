using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("ClientAcknowledgement")]
public class ClientAcknowledgement
{
    public long ClientAcknowledgementID { get; set; }
    public long CustomerID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.Individual;
    public string? SignatureData { get; set; }
    public string? PrintedName { get; set; }
    public DateTime DateSigned { get; set; } = DateTime.UtcNow;
    public bool IsAgreed { get; set; } = true;
}