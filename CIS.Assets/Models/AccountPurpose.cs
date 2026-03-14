using CIS.Assets;
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
    public bool ProductSavings { get; set; }
    public bool ProductTimeDeposit { get; set; }
    public bool ProductSaleOfROPA { get; set; }
    public bool ProductCurrent { get; set; }
    public bool ProductLoan { get; set; }
    public bool ProductOthers { get; set; }
}