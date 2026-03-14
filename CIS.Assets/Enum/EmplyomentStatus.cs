using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum EmploymentStatus
{
    [Description("None")]
    None,
    [Description("Employed")]
    Employed,
    [Description("Self-Employed")]
    SelfEmployed,
    [Description("Unemployed")]
    Unemployed,
    [Description("Others")]
    Others,
}