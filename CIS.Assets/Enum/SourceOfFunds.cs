using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum SourceOfFunds
{
    [Description("None")]
    None,
    [Description("Salary")]
    Salary,
    [Description("Pension")]
    Pension,
    [Description("Business")]
    Business,
    [Description("Regular Remittance")]
    RegularRemittance,
    [Description("Others")]
    Others,
}
