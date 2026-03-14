using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class AccountPurposeConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<AccountPurpose>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.AccountPurposeID);
        entity.IsLong2(e => e.EntityID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarcharEnum(e => e.PurposeOfAccount, 50);
        entity.IsNvarchar(e => e.PurposeOfAccountOther, 255);
        entity.IsBool(e => e.ProductSavings);
        entity.IsBool(e => e.ProductTimeDeposit);
        entity.IsBool(e => e.ProductSaleOfROPA);
        entity.IsBool(e => e.ProductCurrent);
        entity.IsBool(e => e.ProductLoan);
        entity.IsBool(e => e.ProductOthers);
        
        
        
        
    }
}