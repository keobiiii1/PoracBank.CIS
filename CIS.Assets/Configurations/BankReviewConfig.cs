using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class BankReviewConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BankReview>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.BankReviewID);
        entity.IsLong2(e => e.CustomerID);
        entity.IsBool(e => e.IsNegativeListed);
        entity.IsBool(e => e.IsPEPListed);
        entity.IsBool(e => e.IsPoracEmployee);
        entity.IsNvarcharEnum(e => e.DOSRIType, 50);
        entity.IsNvarchar(e => e.EmployeePosition, 255);
        entity.IsBool(e => e.IsRelativeOfEmployee);
        entity.IsNvarchar(e => e.RelativeEmployeeName, 255);
        entity.IsNvarchar(e => e.RelativeEmployeePosition, 255);
        entity.IsNvarchar(e => e.RelativeRelationship, 255);
        entity.IsBool(e => e.IsEntityOwnedByEmployee);
        entity.IsBool(e => e.IsEntityOwnedByPEP);
        entity.IsNvarcharEnum(e => e.NatureOfWorkBusiness, 100);
        entity.IsNvarchar(e => e.NatureOfWorkBusinessOther, 255);
        entity.IsNvarchar(e => e.DocumentsPresented, 500);
        entity.IsNvarchar(e => e.SignatureAuthenticatedBy, 255);
        entity.IsNvarchar(e => e.VerifiedBy, 255);
        entity.IsNvarchar(e => e.ApprovedBy, 255);
        entity.IsNvarchar(e => e.Remarks, 1000);
        entity.Property(e => e.ReviewedAt).HasColumnType("datetime");
        
        
        
        
        entity.HasOne(e => e.Customer).WithOne(e => e.BankReview).HasForeignKey<BankReview>(e => e.CustomerID).OnDelete(DeleteBehavior.Cascade);
    }
}