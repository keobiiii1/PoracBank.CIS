using CIS.Assets;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Assets.Models;

[Table("IndividualIdentification")]
public class IndividualIdentification
{
    public long IdentificationID { get; set; }
    public long CustomerID { get; set; }
    public string? TINNumber { get; set; }
    public string? SSSNumber { get; set; }
    public string? GSISNumber { get; set; }
    public string? DriversLicenseIDNo { get; set; }
    public DateOnly? DriversLicenseExpiry { get; set; }
    public string? PassportIDNo { get; set; }
    public DateOnly? PassportIDExpiry { get; set; }
    public string? OtherIDType { get; set; }
    public string? OtherIDNumber { get; set; }
    public DateOnly? OtherIDExpiry { get; set; }

    // KYC — Selfie (stored as binary in DB)
    public byte[]? SelfieImage { get; set; }
    public string? SelfieContentType { get; set; }

    // KYC — Government ID photos
    public byte[]? TINFrontImage { get; set; }
    public string? TINFrontContentType { get; set; }
    public byte[]? TINBackImage { get; set; }
    public string? TINBackContentType { get; set; }

    public byte[]? SSSFrontImage { get; set; }
    public string? SSSFrontContentType { get; set; }
    public byte[]? SSSBackImage { get; set; }
    public string? SSSBackContentType { get; set; }

    public byte[]? GSISFrontImage { get; set; }
    public string? GSISFrontContentType { get; set; }
    public byte[]? GSISBackImage { get; set; }
    public string? GSISBackContentType { get; set; }

    // KYC — Driver's License photos
    public byte[]? DriversLicenseFrontImage { get; set; }
    public string? DriversLicenseFrontContentType { get; set; }
    public byte[]? DriversLicenseBackImage { get; set; }
    public string? DriversLicenseBackContentType { get; set; }

    // KYC — Passport photo
    public byte[]? PassportFrontImage { get; set; }
    public string? PassportFrontContentType { get; set; }
    public byte[]? PassportBackImage { get; set; }
    public string? PassportBackContentType { get; set; }

    // KYC — Other ID photos
    public byte[]? OtherIDFrontImage { get; set; }
    public string? OtherIDFrontContentType { get; set; }
    public byte[]? OtherIDBackImage { get; set; }
    public string? OtherIDBackContentType { get; set; }

    public Customer Customer { get; set; } = null!;
}