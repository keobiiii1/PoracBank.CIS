using AutoMapper;
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

        // KYC — stored as data URLs (data:image/jpeg;base64,...) for preview + saving
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

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualIdentification, Browse>().ReverseMap();

            // Map binary DB columns → data URLs for display
            CreateMap<IndividualIdentification, PageModel>()
                .ForMember(d => d.SelfieDataUrl, e => e.MapFrom(s => BuildDataUrl(s.SelfieImage, s.SelfieContentType)))
                .ForMember(d => d.TINFrontDataUrl, e => e.MapFrom(s => BuildDataUrl(s.TINFrontImage, s.TINFrontContentType)))
                .ForMember(d => d.TINBackDataUrl, e => e.MapFrom(s => BuildDataUrl(s.TINBackImage, s.TINBackContentType)))
                .ForMember(d => d.SSSFrontDataUrl, e => e.MapFrom(s => BuildDataUrl(s.SSSFrontImage, s.SSSFrontContentType)))
                .ForMember(d => d.SSSBackDataUrl, e => e.MapFrom(s => BuildDataUrl(s.SSSBackImage, s.SSSBackContentType)))
                .ForMember(d => d.GSISFrontDataUrl, e => e.MapFrom(s => BuildDataUrl(s.GSISFrontImage, s.GSISFrontContentType)))
                .ForMember(d => d.GSISBackDataUrl, e => e.MapFrom(s => BuildDataUrl(s.GSISBackImage, s.GSISBackContentType)))
                .ForMember(d => d.DriversLicenseFrontDataUrl, e => e.MapFrom(s => BuildDataUrl(s.DriversLicenseFrontImage, s.DriversLicenseFrontContentType)))
                .ForMember(d => d.DriversLicenseBackDataUrl, e => e.MapFrom(s => BuildDataUrl(s.DriversLicenseBackImage, s.DriversLicenseBackContentType)))
                .ForMember(d => d.PassportFrontDataUrl, e => e.MapFrom(s => BuildDataUrl(s.PassportFrontImage, s.PassportFrontContentType)))
                .ForMember(d => d.PassportBackDataUrl, e => e.MapFrom(s => BuildDataUrl(s.PassportBackImage, s.PassportBackContentType)))
                .ForMember(d => d.OtherIDFrontDataUrl, e => e.MapFrom(s => BuildDataUrl(s.OtherIDFrontImage, s.OtherIDFrontContentType)))
                .ForMember(d => d.OtherIDBackDataUrl, e => e.MapFrom(s => BuildDataUrl(s.OtherIDBackImage, s.OtherIDBackContentType)))
                .ReverseMap()
                .ForMember(d => d.PassportIDExpiry, opt => opt.MapFrom(s => s.PassportIDExpiry));
        }
    }
}