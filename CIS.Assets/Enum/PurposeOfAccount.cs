using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum PurposeOfAccount
{
    [Description("None")]
    [Category("Both")]
    None,

    [Description("Personal Savings")]
    [Category("Individual")]
    PersonalSavings,

    [Description("For Loan Purposes")]
    [Category("Individual")]
    LoanPurposes,

    [Description("Business Operations")]
    [Category("Business")]
    BusinessOperations,

    [Description("Payroll")]
    [Category("Business")]
    Payroll,

    [Description("Investment")]
    [Category("Business")]
    Investment,

    [Description("For Business Purposes")]
    [Category("Both")]
    BusinessPurposes,

    [Description("Others")]
    [Category("Both")]
    Others,
}