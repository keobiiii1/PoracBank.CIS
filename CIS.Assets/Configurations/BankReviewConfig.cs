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
        entity.IsNvarcharEnum(e => e.EntityType, 50);

        entity.IsNvarchar(e => e.CheckedAgainst, 250);

        entity.IsBool(e => e.IsEmployee);
        entity.IsBool(e => e.IsDosri);
        entity.IsBool(e => e.IsRpt);
        entity.IsNvarchar(e => e.Position, 250);

        entity.IsBool(e => e.IsRelative);
        entity.IsNvarchar(e => e.RelativeEmployeeName, 250);
        entity.IsNvarchar(e => e.RelativePosition, 150);
        entity.IsNvarchar(e => e.RelativeRelationship, 100);
        entity.IsBool(e => e.IsEntityOwnedByEmployee);

        entity.IsBool(e => e.IsOwnedByPEP);

        entity.IsNvarcharMax(e => e.NatureOfWorkBusiness);
        entity.IsNvarchar(e => e.NatureOfWorkBusinessOther, 250);

        entity.IsNvarcharMax(e => e.DocumentsPresented);
        entity.IsNvarcharMax(e => e.AdditionalDocuments);
        entity.IsNvarchar(e => e.DocumentsOther, 500);

        entity.IsNvarcharMax(e => e.SignatureAuthenticated);
        entity.IsNvarchar(e => e.VerifiedBy, 250);
        entity.IsNvarchar(e => e.ApprovedBy, 250);
        entity.IsNvarcharMax(e => e.Remarks);
        entity.IsNvarcharMax(e => e.ReviewerSignature);
    }
}