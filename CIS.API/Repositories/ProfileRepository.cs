using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class ProfileRepository
{
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public ProfileRepository(IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task UpsertAsync(AddressDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.Addresses.FirstOrDefaultAsync(e => e.AddressID == request.AddressID);
            if (old == null)
            {
                var model = DtoMapper.ToAddress(request);
                _db.Addresses.Add(model);
            }
            else
            {
                DtoMapper.CopyAddress(request, old);
                _db.Addresses.Update(old);
            }
            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }

    public async Task UpsertAsync(ContactDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        var old = await _db.Contacts
            .FirstOrDefaultAsync(e => e.EntityID == request.EntityID && e.EntityType == request.EntityType);

        if (old == null)
        {
            var model = DtoMapper.ToContact(request);
            _db.Contacts.Add(model);
        }
        else
        {
            DtoMapper.CopyContact(request, old);
            _db.Contacts.Update(old);
        }
        await _db.SaveChangesAsync();
    }
}
