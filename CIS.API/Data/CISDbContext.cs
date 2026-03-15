using Microsoft.EntityFrameworkCore;
using CIS.Assets.Configurations;
using CIS.Assets.Models;

namespace CIS.API.Data;

public class CISDbContext : DbContext
{
    public CISDbContext(DbContextOptions<CISDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<ClientAcknowlegdement> ClientAcknowlegdements { get; set; }
    public DbSet<IndividualInfo> IndividualInfos { get; set; }
    public DbSet<IndividualFamily> IndividualFamilies { get; set; }
    public DbSet<IndividualIdentification> IndividualIdentifications { get; set; }
    public DbSet<IndividualForeigner> IndividualForeigners { get; set; }
    public DbSet<IndividualEmployment> IndividualEmployments { get; set; }
    public DbSet<BusinessInfo> BusinessInfos { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<AccountPurpose> AccountPurposes { get; set; }
    public DbSet<SourceOfFunds> SourceOfFunds { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<BusinessInterest> BusinessInterests { get; set; }
    public DbSet<GovernmentRelation> GovernmentRelations { get; set; }
    public DbSet<BankReview> BankReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Configurations in the "cis" schema
        CustomerConfig.Set(modelBuilder);
        ClientAcknowlegdementConfig.Set(modelBuilder);
        IndividualInfoConfig.Set(modelBuilder);
        IndividualFamilyConfig.Set(modelBuilder);
        IndividualIdentificationConfig.Set(modelBuilder);
        IndividualForeignerConfig.Set(modelBuilder);
        IndividualEmploymentConfig.Set(modelBuilder);
        BusinessInfoConfig.Set(modelBuilder);
        AddressConfig.Set(modelBuilder);
        ContactConfig.Set(modelBuilder);
        AccountPurposeConfig.Set(modelBuilder);
        SourceOfFundsConfig.Set(modelBuilder);
        BeneficiaryConfig.Set(modelBuilder);
        BusinessInterestConfig.Set(modelBuilder);
        GovernmentRelationConfig.Set(modelBuilder);
        BankReviewConfig.Set(modelBuilder);
    }
}