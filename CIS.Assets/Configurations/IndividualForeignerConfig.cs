using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class IndividualForeignerConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<IndividualForeigner>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.ForeignerID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarchar(e => e.PassportIDNumber, 100);
        entity.IsDateOnly(e => e.PassportExpiry);
        entity.IsBool(e => e.IsACR);
        entity.IsBool(e => e.IsSIRV);
        entity.IsBool(e => e.IsSRRV);
    }
}