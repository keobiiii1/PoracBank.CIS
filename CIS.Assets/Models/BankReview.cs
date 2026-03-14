using CIS.Assets;
using CIS.Assets.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("BankReview")]
public class BankReview 
{
    public long BankReviewID { get; set; }
    public long CustomerID { get; set; }
    public bool IsNegativeListed { get; set; }
    public bool IsPEPListed { get; set; }
    public bool IsPoracEmployee { get; set; }
    public DOSRIType DOSRIType { get; set; } = DOSRIType.None;
    public string? EmployeePosition { get; set; }
    public bool IsRelativeOfEmployee { get; set; }
    public string? RelativeEmployeeName { get; set; }
    public string? RelativeEmployeePosition { get; set; }
    public string? RelativeRelationship { get; set; }
    public bool IsEntityOwnedByEmployee { get; set; }
    public bool IsEntityOwnedByPEP { get; set; }
    public NatureOfWorkBusiness NatureOfWorkBusiness { get; set; } = NatureOfWorkBusiness.None;
    public string? NatureOfWorkBusinessOther { get; set; }
    public string? DocumentsPresented { get; set; }
    public string? SignatureAuthenticatedBy { get; set; }
    public string? VerifiedBy { get; set; }
    public string? ApprovedBy { get; set; }
    public string? Remarks { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public Customer Customer { get; set; } = null!;
}