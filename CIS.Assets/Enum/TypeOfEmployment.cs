using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum TypeOfEmployment
{
    [Description("None")]
    None,
    [Description("Government")]
    Government,
    [Description("Private")]
    Private,
    [Description("OFW")]
    OFW,
    [Description("Others")]
    Others,
}
