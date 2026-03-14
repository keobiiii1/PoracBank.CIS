using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum SizeOfBusiness
{
    [Description("None")]
    None,
    [Description("Micro (assets of up to P3M)")]
    Micro,
    [Description("Small (assets of P3,000,001.00 - P15M)")]
    Small,
    [Description("Medium (assets of P15,000,001.00 to P100M)")]
    Medium,
    [Description("Large (assets of more than P100M)")]
    Large,
}
