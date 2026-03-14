using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum AverageMonthlyIncome
{
    [Description("None")]
    None,
    [Description("Under P10,000.00")]
    UnderP10000,
    [Description("P10,001.00 - P20,000.00")]
    P10001ToP20000,
    [Description("P20,001.00 - P50,000.00")]
    P20001ToP50000,
    [Description("P50,001.00 - P100,000.00")]
    P50001ToP100000,
    [Description("P100,001.00 - P200,000.00")]
    P100001ToP200000,
    [Description("Above P200,001.00")]
    AboveP200001,
}
