using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum MailingPreference
{
    [Description("None")]
    None,
    [Description("Permanent Address")]
    PermanentAddress,
    [Description("Present Address")]
    PresentAddress,
}
