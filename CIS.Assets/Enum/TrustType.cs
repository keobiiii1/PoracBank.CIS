using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum TrustType
{
    [Description("None")]
    None,
    [Description("In Trust For")]
    ITF,
    [Description("For the Account Of")]
    FAO
}