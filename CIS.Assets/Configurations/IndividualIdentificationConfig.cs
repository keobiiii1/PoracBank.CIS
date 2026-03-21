using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class IndividualIdentificationConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<IndividualIdentification>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.IdentificationID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarchar(e => e.TINNumber, 50);
        entity.IsNvarchar(e => e.SSSNumber, 50);
        entity.IsNvarchar(e => e.GSISNumber, 50);
        entity.IsNvarchar(e => e.DriversLicenseIDNo, 100);
        entity.IsDateOnly(e => e.DriversLicenseExpiry);
        entity.IsNvarchar(e => e.PassportIDNo, 100);
        entity.IsDateOnly(e => e.PassportIDExpiry);
        entity.IsNvarchar(e => e.OtherIDType, 100);
        entity.IsNvarchar(e => e.OtherIDNumber, 100);
        entity.IsDateOnly(e => e.OtherIDExpiry);
        entity.IsNvarchar(e => e.TINFrontImagePath, 500);
        entity.IsNvarchar(e => e.TINBackImagePath, 500);
        entity.IsNvarchar(e => e.SSSFrontImagePath, 500);
        entity.IsNvarchar(e => e.SSSBackImagePath, 500);
        entity.IsNvarchar(e => e.GSISFrontImagePath, 500);
        entity.IsNvarchar(e => e.GSISBackImagePath, 500);
        entity.IsNvarchar(e => e.SelfieImagePath, 500);
        entity.IsNvarchar(e => e.DriversLicenseFrontImagePath, 500);
        entity.IsNvarchar(e => e.DriversLicenseBackImagePath, 500);
        entity.IsNvarchar(e => e.PassportFrontImagePath, 500);
        entity.IsNvarchar(e => e.PassportBackImagePath, 500);
        entity.IsNvarchar(e => e.OtherIDFrontImagePath, 500);
        entity.IsNvarchar(e => e.OtherIDBackImagePath, 500);
    }
}