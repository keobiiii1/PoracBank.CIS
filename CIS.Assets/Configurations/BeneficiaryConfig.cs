using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class BeneficiaryConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Beneficiary>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.BeneficiaryID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarcharEnum(e => e.TrustType, 50);
        entity.IsNvarchar(e => e.BeneficiaryName, 255);
        entity.IsDateOnly(e => e.Birthday);
        entity.IsNvarchar(e => e.PlaceOfBirth, 255);
        entity.IsNvarchar(e => e.Nationality, 100);
        entity.IsNvarchar(e => e.Address, 500);
        entity.IsNvarchar(e => e.NatureOfWork, 255);

        entity.HasOne(e => e.Customer).WithOne(e => e.Beneficiary).HasForeignKey<Beneficiary>(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}