using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum MaritalStatus
{
    [Description("None")]
    None,
    [Description("Single")]
    Single,
    [Description("Married")]
    Married,
    [Description("Divorced / Annulled")]
    DivorcedAnnulled,
    [Description("Separated")]
    Separated,
    [Description("Widowed")]
    Widowed,
    [Description("Widower")]
    Widower,
}
