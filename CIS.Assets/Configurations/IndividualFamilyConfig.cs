using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class IndividualFamilyConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<IndividualFamily>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.FamilyID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarchar(e => e.SpouseLastName, 100);
        entity.IsNvarchar(e => e.SpouseGivenName, 100);
        entity.IsNvarchar(e => e.SpouseMiddleName, 100);
        entity.IsNvarchar(e => e.MotherMaidenLastName, 100);
        entity.IsNvarchar(e => e.MotherMaidenGivenName, 100);
        entity.IsNvarchar(e => e.MotherMaidenMiddleName, 100);
        
        
        
        
        entity.HasOne(e => e.Customer).WithOne(e => e.IndividualFamily).HasForeignKey<IndividualFamily>(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}