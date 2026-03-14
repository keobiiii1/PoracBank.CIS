using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum EntityType
{
    [Description("None")]
    None,
    [Description("Individual")]
    Individual,
    [Description("Business")]
    Business,
    [Description("Beneficiary")]
    Beneficiary,
}
