using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using CIS.Assets.Enum;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class BusinessRepository
{
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public BusinessRepository(IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task<long> BusinessFullRegistration(BusinessRegistrationRequest request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            // 1. Create the Master Customer Record
            // Generate unique CIDNumber: BUS + 7 digits (e.g. BUS0000001)
            var cidNumber = await GenerateCIDNumberAsync(_db, "BUS");

            var customer = new Customer
            {
                EntityType = EntityType.Business,
                CIDNumber = cidNumber,
                CreatedAt = DateTime.UtcNow
            };
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync(); // Generates CustomerID

            long customerId = customer.CustomerID;

            // 2. Map and Add Main Business Info
            var bizInfo = DtoMapper.ToBusinessInfo(request.Business);
            bizInfo.CustomerID = customerId;
            _db.BusinessInfos.Add(bizInfo);

            // 3. Save Address
            var address = DtoMapper.ToAddress(request.Address);
            address.EntityID = customerId;
            address.EntityType = EntityType.Business;
            _db.Addresses.Add(address);

            // 4. Save Contact (Using Phone/Email fields from Business model)
            _db.Contacts.Add(new Contact
            {
                EntityID = customerId,
                EntityType = EntityType.Business,
                EmailAddress = request.Business.EmailAddress,
                MobilePhoneNumber = request.Business.OfficePhoneNo,
                ContactPerson = request.Business.ContactPerson
            });

            // 5. Save Beneficiary (if any)
            if (!string.IsNullOrEmpty(request.Beneficiary.BeneficiaryName))
            {
                var beneficiary = DtoMapper.ToBeneficiary(request.Beneficiary);
                beneficiary.CustomerID = customerId;
                beneficiary.EntityType = EntityType.Business;
                _db.Beneficiaries.Add(beneficiary);
            }

            // 6. Save Acknowledgement (Signature)
            var ack = DtoMapper.ToClientAcknowledgement(request.Acknowledgement);
            ack.CustomerID = customerId;
            ack.EntityType = EntityType.Business;
            _db.ClientAcknowledgements.Add(ack);

            // 7. Save Bank Review
            var review = DtoMapper.ToBankReview(request.BankReview);
            review.CustomerID = customerId;
            review.EntityType = EntityType.Business;
            _db.BankReviews.Add(review);

            // 8. Save Account Purpose
            _db.AccountPurposes.Add(new AccountPurpose
            {
                EntityID = customerId,
                EntityType = EntityType.Business,
                PurposeOfAccount = request.Business.AccountPurpose,
                PurposeOfAccountOther = request.Business.AccountPurposeOther,
                ProductsAvailed = request.Business.ProductsAvailed,
                ProductsAvailedOther = request.Business.ProductsAvailedOther
            });

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();

            return customerId;
        }
        catch (Exception)
        {
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
    }

    private static async Task<string> GenerateCIDNumberAsync(
        CISDbContext db, string prefix)
    {
        var last = await db.Customers
            .Where(c => c.CIDNumber != null && c.CIDNumber.StartsWith(prefix))
            .OrderByDescending(c => c.CIDNumber)
            .Select(c => c.CIDNumber)
            .FirstOrDefaultAsync();

        int next = 1;
        if (last != null && last.Length > prefix.Length)
        {
            if (int.TryParse(last[prefix.Length..], out int parsed))
                next = parsed + 1;
        }

        return $"{prefix}{next:D7}";
    }
}
