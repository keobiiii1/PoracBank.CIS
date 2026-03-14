using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class BusinessInfoConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BusinessInfo>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.BusinessInfoID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsNvarchar(e => e.NameOfBusiness, 255);
        entity.IsBool(e => e.IsGovernment);
        entity.IsBool(e => e.IsPrivate);
        entity.IsNvarcharEnum(e => e.TypeOfOrganization, 50);
        entity.IsNvarchar(e => e.TypeOfOrganizationOther, 255);
        entity.IsNvarchar(e => e.BusinessAddress, 500);
        entity.IsNvarchar(e => e.PrincipalAddress, 500);
        entity.IsDateOnly(e => e.DateOfRegistration);
        entity.IsNvarchar(e => e.BusinessRegNumber, 100);
        entity.IsDateOnly(e => e.BusinessRegExpiry);
        entity.IsNvarchar(e => e.PlaceOfRegistration, 255);
        entity.IsNvarchar(e => e.NatureOfBusiness, 255);
        entity.IsNvarchar(e => e.TaxIdentificationNumber, 50);
        entity.IsNvarchar(e => e.DTICertNumber, 100);
        entity.IsDateOnly(e => e.DTICertExpiry);
        entity.IsNvarcharEnum(e => e.SizeOfBusiness, 50);
        entity.IsNvarcharEnum(e => e.AverageAnnualIncome, 50);
        
        
        
        
        entity.HasOne(e => e.Customer).WithOne(e => e.BusinessInfo).HasForeignKey<BusinessInfo>(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}