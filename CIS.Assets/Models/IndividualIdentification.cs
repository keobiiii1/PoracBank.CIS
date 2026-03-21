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

    // KYC — Government ID photos (shown when TIN / SSS / GSIS has a value)
    public string? TINFrontImagePath { get; set; }
    public string? TINBackImagePath { get; set; }
    public string? SSSFrontImagePath { get; set; }
    public string? SSSBackImagePath { get; set; }
    public string? GSISFrontImagePath { get; set; }
    public string? GSISBackImagePath { get; set; }

    // KYC — Selfie
    public string? SelfieImagePath { get; set; }

    // KYC — Driver's License photos
    public string? DriversLicenseFrontImagePath { get; set; }
    public string? DriversLicenseBackImagePath { get; set; }

    // KYC — Passport photo
    public string? PassportFrontImagePath { get; set; }
    public string? PassportBackImagePath { get; set; }

    // KYC — Other ID photos
    public string? OtherIDFrontImagePath { get; set; }
    public string? OtherIDBackImagePath { get; set; }

    public Customer Customer { get; set; } = null!;
}