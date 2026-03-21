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

        // KYC binary image columns
        entity.IsVarbinaryMax(e => e.SelfieImage);
        entity.IsNvarchar(e => e.SelfieContentType, 50);

        entity.IsVarbinaryMax(e => e.TINFrontImage);
        entity.IsNvarchar(e => e.TINFrontContentType, 50);
        entity.IsVarbinaryMax(e => e.TINBackImage);
        entity.IsNvarchar(e => e.TINBackContentType, 50);

        entity.IsVarbinaryMax(e => e.SSSFrontImage);
        entity.IsNvarchar(e => e.SSSFrontContentType, 50);
        entity.IsVarbinaryMax(e => e.SSSBackImage);
        entity.IsNvarchar(e => e.SSSBackContentType, 50);

        entity.IsVarbinaryMax(e => e.GSISFrontImage);
        entity.IsNvarchar(e => e.GSISFrontContentType, 50);
        entity.IsVarbinaryMax(e => e.GSISBackImage);
        entity.IsNvarchar(e => e.GSISBackContentType, 50);

        entity.IsVarbinaryMax(e => e.DriversLicenseFrontImage);
        entity.IsNvarchar(e => e.DriversLicenseFrontContentType, 50);
        entity.IsVarbinaryMax(e => e.DriversLicenseBackImage);
        entity.IsNvarchar(e => e.DriversLicenseBackContentType, 50);

        entity.IsVarbinaryMax(e => e.PassportFrontImage);
        entity.IsNvarchar(e => e.PassportFrontContentType, 50);
        entity.IsVarbinaryMax(e => e.PassportBackImage);
        entity.IsNvarchar(e => e.PassportBackContentType, 50);

        entity.IsVarbinaryMax(e => e.OtherIDFrontImage);
        entity.IsNvarchar(e => e.OtherIDFrontContentType, 50);
        entity.IsVarbinaryMax(e => e.OtherIDBackImage);
        entity.IsNvarchar(e => e.OtherIDBackContentType, 50);
    }
}