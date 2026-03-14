using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class CustomerConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Customer>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.CustomerID);
        entity.IsNvarcharEnum(e => e.CustomerCategory, 50);
        entity.IsNvarcharEnum(e => e.CustomerType, 50);
        entity.IsNvarchar(e => e.CIDNumber, 50);
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        
        
        
    }
}