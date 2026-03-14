using CIS.Assets.Components.MSSql;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS.Assets.Configurations;

public static class ContactConfig
{
    public static void Set(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Contact>();
        entity.AsTable("cis");
        entity.IsPrimaryKey(e => e.ContactID);
        entity.IsLong2(e => e.EntityID);
        entity.IsNvarcharEnum(e => e.EntityType, 50);
        entity.IsNvarchar(e => e.HomePhoneNumber, 50);
        entity.IsNvarchar(e => e.MobilePhoneNumber, 50);
        entity.IsNvarchar(e => e.EmailAddress, 255);
        entity.IsNvarchar(e => e.ContactPerson, 255);
        
        
        
        
    }
}