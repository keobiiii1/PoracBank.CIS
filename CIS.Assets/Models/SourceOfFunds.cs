using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("SourceOfFunds")]
public class SourceOfFunds 
{
    public long SourceOfFundsID { get; set; }
    public long EntityID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.None;
    public Enum.SourceOfFunds SourceOfFundsType { get; set; } = Enum.SourceOfFunds.None;
    public string? SourceOfFundsOther { get; set; }
}