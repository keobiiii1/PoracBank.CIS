using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("AccountPurpose")]
public class AccountPurpose
{
    public long AccountPurposeID { get; set; }
    public long EntityID { get; set; }
    public EntityType EntityType { get; set; } = EntityType.None;
    public PurposeOfAccount PurposeOfAccount { get; set; } = PurposeOfAccount.None;
    public string? PurposeOfAccountOther { get; set; }
    public string? ProductsAvailed { get; set; }
    public string? ProductsAvailedOther { get; set; }
}