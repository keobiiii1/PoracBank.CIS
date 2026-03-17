using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class IndividualInfoConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<IndividualInfo>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.IndividualInfoID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarchar(e => e.FirstName, 100);
        entity.IsNvarchar(e => e.MiddleName, 100);
        entity.IsNvarchar(e => e.LastName, 100);
        entity.IsNvarchar(e => e.MaidenName, 100);
        entity.IsNvarchar(e => e.MaidenMiddleName, 100);
        entity.IsNvarchar(e => e.MaidenLastName, 100);
        entity.IsBool(e => e.IsResident);
        entity.IsBool(e => e.IsGov);
        entity.IsNvarchar(e => e.GovPosition, 100);
        entity.IsNvarchar(e => e.GovPeriod, 100);
        entity.IsNvarcharEnum(e => e.Citizenship, 50);
        entity.IsNvarchar(e => e.CitizenshipOther, 100);
        entity.IsNvarchar(e => e.CountryOfOrigin, 100);
        entity.IsDateOnly(e => e.DateOfBirth);
        entity.IsNvarchar(e => e.PlaceOfBirth, 255);
        entity.IsNvarcharEnum(e => e.Gender, 20);
        entity.IsNvarcharEnum(e => e.MaritalStatus, 50);
        entity.IsNvarcharEnum(e => e.MailingPreference, 50);
        entity.HasOne(e => e.Customer).WithOne(e => e.IndividualInfo).HasForeignKey<IndividualInfo>(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}