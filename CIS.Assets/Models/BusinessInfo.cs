using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("BusinessInfo")]
public class BusinessInfo 
{
    public long BusinessInfoID { get; set; }
    public long CustomerID { get; set; }
    public string? NameOfBusiness { get; set; }
    public bool IsGovernment { get; set; }
    public bool IsPrivate { get; set; }
    public TypeOfOrganization TypeOfOrganization { get; set; } = TypeOfOrganization.None;
    public string? TypeOfOrganizationOther { get; set; }
    public string? BusinessAddress { get; set; }
    public string? PrincipalAddress { get; set; }
    public DateOnly? DateOfRegistration { get; set; }
    public string? BusinessRegNumber { get; set; }
    public DateOnly? BusinessRegExpiry { get; set; }
    public string? PlaceOfRegistration { get; set; }
    public string? NatureOfBusiness { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string? DTICertNumber { get; set; }
    public DateOnly? DTICertExpiry { get; set; }
    public SizeOfBusiness SizeOfBusiness { get; set; } = SizeOfBusiness.None;
    public AverageAnnualIncome AverageAnnualIncome { get; set; } = AverageAnnualIncome.None;
    public Customer Customer { get; set; } = null!; 
}