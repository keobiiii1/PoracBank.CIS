using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum CustomerCategory
{
    [Description("None")]
    None,
    [Description("Loan Borrower")]
    LoanBorrower,
    [Description("Loan Co-Maker")]
    LoanCoMaker,
    [Description("Depositor")]
    Depositor,
}