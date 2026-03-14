using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum CustomerType
{
    [Description("None")]
    None,
    [Description("Walk-in")]
    WalkIn,
    [Description("Solicited")]
    Solicited,
    [Description("Referred")]
    Referred,
}
