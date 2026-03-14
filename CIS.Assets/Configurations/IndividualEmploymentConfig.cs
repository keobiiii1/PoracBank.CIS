using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class IndividualEmploymentConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<IndividualEmployment>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.EmploymentID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarcharEnum(e => e.EmploymentStatus, 50);
        entity.IsNvarchar(e => e.EmploymentStatusOther, 255);
        entity.IsNvarcharEnum(e => e.TypeOfEmployment, 50);
        entity.IsNvarchar(e => e.TypeOfEmploymentOther, 255);
        entity.IsNvarchar(e => e.OFWCountry, 100);
        entity.IsNvarchar(e => e.EducationalAttainment, 255);
        entity.IsNvarchar(e => e.NatureOfWork, 255);
        entity.IsNvarcharEnum(e => e.AverageMonthlyIncome, 50);
        entity.IsNvarchar(e => e.NameOfEmployer, 255);
        entity.IsNvarchar(e => e.EmployerBuildingNo, 100);
        entity.IsNvarchar(e => e.EmployerStreet, 255);
        entity.IsNvarchar(e => e.EmployerBrgyDistrict, 255);
        entity.IsNvarchar(e => e.EmployerCityTown, 255);
        entity.IsNvarchar(e => e.EmployerPhoneNumber, 50);
        entity.IsNvarchar(e => e.EmployerEmailAddress, 255);
        entity.IsNvarchar(e => e.PositionRank, 255);
    }
}