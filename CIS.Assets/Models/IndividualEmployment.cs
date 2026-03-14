using CIS.Assets;
using CIS.Assets.Enum;

namespace CIS.Assets.Models;

public class IndividualEmployment 
{
    public long EmploymentID { get; set; }
    public long CustomerID { get; set; }
    public EmploymentStatus EmploymentStatus { get; set; } = EmploymentStatus.None;
    public string? EmploymentStatusOther { get; set; }
    public TypeOfEmployment TypeOfEmployment { get; set; } = TypeOfEmployment.None;
    public string? TypeOfEmploymentOther { get; set; }
    public string? OFWCountry { get; set; }
    public string? EducationalAttainment { get; set; }
    public string? NatureOfWork { get; set; }
    public AverageMonthlyIncome AverageMonthlyIncome { get; set; } = AverageMonthlyIncome.None;
    public string? NameOfEmployer { get; set; }
    public string? EmployerBuildingNo { get; set; }
    public string? EmployerStreet { get; set; }
    public string? EmployerBrgyDistrict { get; set; }
    public string? EmployerCityTown { get; set; }
    public string? EmployerPhoneNumber { get; set; }
    public string? EmployerEmailAddress { get; set; }
    public string? PositionRank { get; set; }
    public Customer Customer { get; set; } = null!;
}