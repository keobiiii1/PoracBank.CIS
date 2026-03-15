using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("ClientAcknowlegdement")]
public class ClientAcknowlegdement
{
    public long ClientAcknowlegdementID { get; set; }
    public long CustomerID { get; set; }
    public string? SignatureData { get; set; }
    public string? PrintedName { get; set; }

    public DateTime DateSigned { get; set; } = DateTime.UtcNow;
    public bool IsAgreed { get; set; } = true;
}