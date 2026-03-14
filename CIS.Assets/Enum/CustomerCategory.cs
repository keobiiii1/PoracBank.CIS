using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum CustomerCategory
{
    [Description("None")]
    None,
    [Description("Borrower")]
    Borrower,
    [Description("Non-Borrower")]
    NonBorrower,
}
