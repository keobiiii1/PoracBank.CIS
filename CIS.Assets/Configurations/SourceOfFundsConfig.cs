using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class SourceOfFundsConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<SourceOfFunds>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.SourceOfFundsID);
        entity.IsLong2(e => e.EntityID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarcharEnum(e => e.SourceOfFundsType, 50);
        entity.IsNvarchar(e => e.SourceOfFundsOther, 255);
        
        
        
        
    }
}