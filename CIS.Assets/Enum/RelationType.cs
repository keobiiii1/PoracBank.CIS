using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum RelationType
{
    [Description("None")]
    None,
    [Description("Self (Past/Present Official)")]
    Self,
    [Description("Relative of Official")]
    Relative,
}
