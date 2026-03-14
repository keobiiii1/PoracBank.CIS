using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class ProfileRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public ProfileRepository(IMapper mapper, IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
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
                var model = _mapper.Map<Address>(request);
                _db.Addresses.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
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
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.Contacts.FirstOrDefaultAsync(e => e.ContactID == request.ContactID);
            if (old == null)
            {
                var model = _mapper.Map<Contact>(request);
                _db.Contacts.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
                _db.Contacts.Update(old);
            }
            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }
}