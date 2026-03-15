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
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarcharEnum(e => e.CustomerCategory, 50);
        entity.IsNvarcharEnum(e => e.CustomerType, 50);
        entity.IsNvarchar(e => e.CIDNumber, 50);
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        entity.HasOne(e => e.ClientAcknowlegdement)
            .WithOne()
            .HasForeignKey<ClientAcknowlegdement>(e => e.CustomerID);

        entity.HasOne(e => e.BankReview)
            .WithOne()
            .HasForeignKey<BankReview>(e => e.CustomerID);
    }
}