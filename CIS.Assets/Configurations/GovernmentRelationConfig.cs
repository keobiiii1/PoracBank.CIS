using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class GovernmentRelationConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<GovernmentRelation>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.GovernmentRelationID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarcharEnum(e => e.RelationType, 50);
        entity.IsNvarchar(e => e.Name, 255);
        entity.IsNvarchar(e => e.Relationship, 255);
        entity.IsNvarchar(e => e.HighestPositionOccupied, 255);
        entity.IsNvarchar(e => e.PeriodCovered, 100);

        entity.HasOne(e => e.Customer).WithMany(e => e.GovernmentRelations).HasForeignKey(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}