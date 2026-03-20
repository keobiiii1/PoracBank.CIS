using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("IndividualInfo")]
public class IndividualInfo
{
    public long IndividualInfoID { get; set; }
    public long CustomerID { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? MaidenName { get; set; }
    public string? MaidenMiddleName { get; set; }
    public string? MaidenLastName { get; set; }
    public bool IsResident { get; set; }
    public bool IsGov { get; set; }
    public string? GovPosition { get; set; }
    public string? GovPeriod { get; set; }
    public Citizenship Citizenship { get; set; } = Citizenship.None;
    public string? CitizenshipOther { get; set; }
    public string? CountryOfOrigin { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }
    public Gender Gender { get; set; } = Gender.None;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.None;
    public Customer Customer { get; set; } = null!;
}