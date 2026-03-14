using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class BusinessRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public BusinessRepository(IMapper mapper, IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task UpsertAsync(BusinessInfoDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.BusinessInfos.FirstOrDefaultAsync(e => e.BusinessInfoID == request.BusinessInfoID);
            if (old == null)
            {
                var model = _mapper.Map<BusinessInfo>(request);
                _db.BusinessInfos.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
                _db.BusinessInfos.Update(old);
            }
            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }

    public async Task UpsertAsync(BusinessInterestDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.BusinessInterests.FirstOrDefaultAsync(e => e.BusinessInterestID == request.BusinessInterestID);
            if (old == null)
            {
                var model = _mapper.Map<BusinessInterest>(request);
                _db.BusinessInterests.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
                _db.BusinessInterests.Update(old);
            }
            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }
}