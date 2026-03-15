using CIS.Assets.Enum;

namespace CIS.Assets.Models;

public class BankReview
{
    public long BankReviewID { get; set; }
    public long CustomerID { get; set; }
    public string? CheckedAgainst { get; set; }
    public bool IsEmployee { get; set; }
    public bool IsDosri { get; set; }
    public bool IsRpt { get; set; }
    public string? Position { get; set; }
    public bool IsRelative { get; set; }
    public string? RelativeEmployeeName { get; set; }
    public string? RelativePosition { get; set; }
    public string? RelativeRelationship { get; set; }

    public bool IsEntityOwnedByEmployee { get; set; }
    public string? NatureOfWorkBusiness { get; set; }
    public string? NatureOfWorkBusinessOther { get; set; }
    public bool IsOwnedByPEP { get; set; }
    public string? DocumentsPresented { get; set; }
    public string? AdditionalDocuments { get; set; }
    public string? DocumentsOther { get; set; }
    public string? SignatureAuthenticated { get; set; }
    public string? VerifiedBy { get; set; }
    public string? ApprovedBy { get; set; }
    public string? Remarks { get; set; }
    public string? ReviewerSignature { get; set; }
}