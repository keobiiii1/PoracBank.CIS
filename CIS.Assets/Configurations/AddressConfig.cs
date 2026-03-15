using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class AddressConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Address>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.AddressID);
        entity.IsLong2(e => e.EntityID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);

        // Individual Mapping
        entity.IsNvarchar(e => e.PermanentAddress, 500);
        entity.IsNvarchar(e => e.PermanentZipCode, 20);
        entity.IsNvarchar(e => e.PermanentCountry, 100);
        entity.IsNvarchar(e => e.PresentAddress, 500);
        entity.IsNvarchar(e => e.PresentZipCode, 20);
        entity.IsNvarchar(e => e.PresentCountry, 100);

        // New Business Mapping
        entity.IsNvarchar(e => e.BusinessAddress, 500);
        entity.IsNvarchar(e => e.PrincipalAddress, 500);
    }
}