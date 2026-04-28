using CIS.API.Data;
using CIS.Assets.Common;
using CIS.Assets.Models;
using Codex.Components.DataModels.CustomExceptions;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

/// <summary>
/// Refactored Repository: Now strictly for Customer Management (Browse/Delete).
/// Creating new customers is now handled by Individual/Business batch submission.
/// </summary>
public class CustomerRepository
{
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public CustomerRepository(IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task<CollectionDataSet<CustomerDTO.Browse>> BrowseAsync(CustomerDTO.Filter filter)
    {
        using var _dbcontext = _dbContextFactory.CreateDbContext();
        var response = new CollectionDataSet<CustomerDTO.Browse>();

        var query = _dbcontext.Customers.AsNoTracking();

        if (!string.IsNullOrEmpty(filter.SearchText))
        {
            query = query.Where(e => e.CIDNumber != null && EF.Functions.Like(e.CIDNumber, $"%{filter.SearchText}%"));
        }

        var data = await query
            .Select(e => new CustomerDTO.Browse
            {
                CustomerID = e.CustomerID,
                CIDNumber = e.CIDNumber,
                CustomerCategories = e.CustomerCategories,
            })
            .Skip((filter.CurrentPage - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        response.TotalRecords = await query.CountAsync();
        response.TotalPages = (int)Math.Ceiling(response.TotalRecords / (double)filter.PageSize);
        response.Data = data;
        return response;
    }

    /// <summary>
    /// Update an existing customer record. 
    /// Note: Creating new customers is now atomic within the Registration flows.
    /// </summary>
    public async Task<long> UpdateAsync(CustomerDTO.PageModel request)
    {
        using var _dbcontext = _dbContextFactory.CreateDbContext();
        using var transaction = await _transactionPolicy.BeginSqlTransaction(_dbcontext);
        try
        {
            var old = await _dbcontext.Customers.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (old == null) throw new XNotAcceptableException("Customer record not found for update.");

            old.CustomerCategories = request.CustomerCategories;
            old.CIDNumber = request.CIDNumber;

            _dbcontext.Customers.Update(old);
            await _dbcontext.SaveChangesAsync();

            if (transaction is not null) await transaction.CommitAsync();
            return request.CustomerID;
        }
        catch (Exception)
        {
            if (transaction is not null) await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(long customerId)
    {
        using var _dbcontext = _dbContextFactory.CreateDbContext();
        using var transaction = await _transactionPolicy.BeginSqlTransaction(_dbcontext);
        try
        {
            var model = await _dbcontext.Customers.FindAsync(customerId);
            if (model == null) throw new XNotAcceptableException("Customer record not found.");

            _dbcontext.Customers.Remove(model);

            await _dbcontext.SaveChangesAsync();
            if (transaction is not null) await transaction.CommitAsync();
        }
        catch (Exception)
        {
            if (transaction is not null) await transaction.RollbackAsync();
            throw;
        }
    }
}
