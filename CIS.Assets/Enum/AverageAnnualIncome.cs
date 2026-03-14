using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum AverageAnnualIncome
{
    [Description("None")]
    None,
    [Description("Below P100,000.00")]
    BelowP100000,
    [Description("P100,001.00 - P500,000.00")]
    P100001ToP500000,
    [Description("P500,001.00 - P1M")]
    P500001ToP1M,
    [Description("More than P1M")]
    MoreThanP1M,
}
