using AutoMapper;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class BankReviewDTO
{
    public class PageModel
    {
        public long BankReviewID { get; set; }
        public long CustomerID { get; set; }
        public List<string> ScreeningList { get; set; } = new();
        public List<string> WorkList { get; set; } = new();
        public List<string> DocsList { get; set; } = new();
        public List<string> AdditionalDocsList { get; set; } = new();
        public bool IsEmployee { get; set; }
        public bool IsDosri { get; set; }
        public bool IsRpt { get; set; }
        public string? Position { get; set; }
        public bool IsRelative { get; set; }
        public string? RelativeEmployeeName { get; set; }
        public string? RelativePosition { get; set; }
        public string? RelativeRelationship { get; set; }

        public bool IsEntityOwnedByEmployee { get; set; }
        public string? NatureOfWorkBusinessOther { get; set; }
        public bool IsOwnedByPEP { get; set; }
        public string? DocumentsOther { get; set; }
        public string? SignatureAuthenticated { get; set; }
        public string? VerifiedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? Remarks { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PageModel, BankReview>()
                .ForMember(dest => dest.BankReviewID, opt => opt.Ignore())
                .ForMember(dest => dest.CheckedAgainst, opt => opt.MapFrom(src => string.Join(",", src.ScreeningList)))
                .ForMember(dest => dest.NatureOfWorkBusiness, opt => opt.MapFrom(src => string.Join(",", src.WorkList)))
                .ForMember(dest => dest.DocumentsPresented, opt => opt.MapFrom(src => string.Join(",", src.DocsList)))
                .ForMember(dest => dest.AdditionalDocuments, opt => opt.MapFrom(src => string.Join(",", src.AdditionalDocsList)));

            CreateMap<BankReview, PageModel>();
        }
    }
}