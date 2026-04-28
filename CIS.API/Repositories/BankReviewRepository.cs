using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class BankReviewRepository
{
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public BankReviewRepository(IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task UpsertAsync(BankReviewDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.BankReviews.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

            if (old == null)
            {
                var model = DtoMapper.ToBankReview(request);
                _db.BankReviews.Add(model);
            }
            else
            {
                DtoMapper.CopyBankReview(request, old);

                _db.Entry(old).Property(x => x.BankReviewID).IsModified = false;

                _db.BankReviews.Update(old);
            }

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch
        {
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
    }
}
