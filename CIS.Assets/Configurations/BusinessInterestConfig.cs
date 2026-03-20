using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class BusinessInterestConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BusinessInterest>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.BusinessInterestID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarchar(e => e.BusinessName, 255);
        entity.IsDecimal(e => e.OwnershipPercentage, 5, 2);

        entity.HasOne(e => e.Customer).WithMany(e => e.BusinessInterests).HasForeignKey(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}