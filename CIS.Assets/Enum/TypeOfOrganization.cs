using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum TypeOfOrganization
{
    [Description("None")]
    None,
    [Description("Sole Proprietorship")]
    SoleProprietorship,
    [Description("Partnership")]
    Partnership,
    [Description("Corporation")]
    Corporation,
    [Description("Cooperative")]
    Cooperative,
    [Description("Association")]
    Association,
    [Description("Others")]
    Others,
}
