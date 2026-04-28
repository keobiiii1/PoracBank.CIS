using System.ComponentModel.DataAnnotations;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualIdentificationDTO
{
    public class Browse
    {
        public long IdentificationID { get; set; }
        public string? TINNumber { get; set; }
        public string? SSSNumber { get; set; }
    }

    public class PageModel
    {
        public long IdentificationID { get; set; }
        public long CustomerID { get; set; }

        [MaxLength(50)]
        public string? TINNumber { get; set; }
        [MaxLength(50)]
        public string? SSSNumber { get; set; }
        [MaxLength(50)]
        public string? GSISNumber { get; set; }

        [MaxLength(50)]
        public string? DriversLicenseIDNo { get; set; }
        public DateOnly? DriversLicenseExpiry { get; set; }

        [MaxLength(50)]
        public string? PassportIDNo { get; set; }
        public DateOnly? PassportIDExpiry { get; set; }

        [MaxLength(100)]
        public string? OtherIDType { get; set; }
        [MaxLength(100)]
        public string? OtherIDNumber { get; set; }
        public DateOnly? OtherIDExpiry { get; set; }

        // KYC - stored as data URLs (data:image/jpeg;base64,...) for preview + saving
        // On save these are split into byte[] + content type before writing to DB
        public string? SelfieDataUrl { get; set; }
        public string? TINFrontDataUrl { get; set; }
        public string? TINBackDataUrl { get; set; }
        public string? SSSFrontDataUrl { get; set; }
        public string? SSSBackDataUrl { get; set; }
        public string? GSISFrontDataUrl { get; set; }
        public string? GSISBackDataUrl { get; set; }
        public string? DriversLicenseFrontDataUrl { get; set; }
        public string? DriversLicenseBackDataUrl { get; set; }
        public string? PassportFrontDataUrl { get; set; }
        public string? PassportBackDataUrl { get; set; }
        public string? OtherIDFrontDataUrl { get; set; }
        public string? OtherIDBackDataUrl { get; set; }
    }

    /// <summary>
    /// Splits a data URL into (bytes, contentType). Returns (null, null) if input is null.
    /// </summary>
    public static (byte[]? bytes, string? contentType) ParseDataUrl(string? dataUrl)
    {
        if (string.IsNullOrWhiteSpace(dataUrl)) return (null, null);
        // format: data:{contentType};base64,{base64data}
        var comma = dataUrl.IndexOf(',');
        if (comma < 0) return (null, null);
        var header = dataUrl[5..comma]; // strip "data:"
        var semi = header.IndexOf(';');
        var ct = semi > 0 ? header[..semi] : header;
        var b64 = dataUrl[(comma + 1)..];
        return (Convert.FromBase64String(b64), ct);
    }

    /// <summary>
    /// Builds a data URL from bytes + content type for display in <img src="..."/>.
    /// </summary>
    public static string? BuildDataUrl(byte[]? bytes, string? contentType)
    {
        if (bytes == null || bytes.Length == 0) return null;
        return $"data:{contentType ?? "image/jpeg"};base64,{Convert.ToBase64String(bytes)}";
    }
}