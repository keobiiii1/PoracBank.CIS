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
    }
}