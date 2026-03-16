using AutoMapper;
using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualEmploymentDTO
{
    public class Browse
    {
        public long EmploymentID { get; set; }
        public string? NameOfEmployer { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
    }

    public class PageModel
    {
        public long EmploymentID { get; set; }
        public long CustomerID { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public string? EmploymentStatusOther { get; set; }
        public TypeOfEmployment TypeOfEmployment { get; set; }
        public string? TypeOfEmploymentOther { get; set; }
        public string? OFWCountry { get; set; }
        public string? EducationalAttainment { get; set; }
        public string? NatureOfWork { get; set; }
        public AverageMonthlyIncome AverageMonthlyIncome { get; set; }
        public SourceOfFunds SourceOfFunds { get; set; }
        public string? SourceOfFundsOther { get; set; }
        public string? NameOfEmployer { get; set; }
        public string? EmployerBuildingNo { get; set; }
        public string? EmployerStreet { get; set; }
        public string? EmployerBrgyDistrict { get; set; }
        public string? EmployerCityTown { get; set; }
        public string? EmployerPhoneNumber { get; set; }
        public string? EmployerEmailAddress { get; set; }
        public string? PositionRank { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IndividualEmployment, Browse>().ReverseMap();
            CreateMap<IndividualEmployment, PageModel>().ReverseMap();
        }
    }
}