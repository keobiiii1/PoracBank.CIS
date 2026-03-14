using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class BankReviewRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public BankReviewRepository(IMapper mapper, IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task UpsertAsync(BankReviewDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.BankReviews.FirstOrDefaultAsync(e => e.BankReviewID == request.BankReviewID);
            if (old == null)
            {
                var model = _mapper.Map<BankReview>(request);
                _db.BankReviews.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
                _db.BankReviews.Update(old);
            }
            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }
}