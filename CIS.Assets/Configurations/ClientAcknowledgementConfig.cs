using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class ClientAcknowledgementConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ClientAcknowledgement>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.ClientAcknowledgementID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);

        entity.IsNvarcharMax(e => e.SignatureData);
        entity.IsNvarchar(e => e.PrintedName, 250);

        entity.Property(e => e.DateSigned).HasDefaultValueSql("GETDATE()");
        entity.IsBool(e => e.IsAgreed);
    }
}