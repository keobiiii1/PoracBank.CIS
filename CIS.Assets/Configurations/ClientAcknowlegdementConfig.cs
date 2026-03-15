using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class ClientAcknowlegdementConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ClientAcknowlegdement>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.ClientAcknowlegdementID);
        entity.IsLong2(e => e.CustomerID);

        entity.IsNvarcharMax(e => e.SignatureData);

        entity.IsNvarchar(e => e.PrintedName, 250);

        entity.Property(e => e.DateSigned).HasDefaultValueSql("GETDATE()");
        entity.IsBool(e => e.IsAgreed);
    }
}