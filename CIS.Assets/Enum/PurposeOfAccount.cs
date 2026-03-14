using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum PurposeOfAccount
{
    [Description("None")]
    None,
    [Description("Personal Savings")]
    PersonalSavings,
    [Description("For Loan Purposes")]
    LoanPurposes,
    [Description("For Business Purposes")]
    BusinessPurposes,
    [Description("Others")]
    Others,
}