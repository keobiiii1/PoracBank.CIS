using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Common;
using CIS.Assets.Models;
using Codex.Components.DataModels.CustomExceptions;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class CustomerRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public CustomerRepository(IMapper mapper, IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task<CollectionDataSet<CustomerDTO.Browse>> BrowseAsync(CustomerDTO.Filter filter)
    {
        using var _dbcontext = _dbContextFactory.CreateDbContext();
        var response = new CollectionDataSet<CustomerDTO.Browse>();

        // FIX: Changed .Customers to .Customer
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
                CustomerCategory = e.CustomerCategory,
                CustomerType = e.CustomerType
            })
            .Skip((filter.CurrentPage - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        response.TotalRecords = await query.CountAsync();
        response.TotalPages = (int)Math.Ceiling(response.TotalRecords / (double)filter.PageSize);
        response.Data = data;
        return response;
    }

    public async Task<long> UpsertAsync(CustomerDTO.PageModel request)
    {
        using var _dbcontext = _dbContextFactory.CreateDbContext();
        using var transaction = await _transactionPolicy.BeginSqlTransaction(_dbcontext);
        try
        {
            var validator = new CustomerDTO.PageModel.Validator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new XNotAcceptableException(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            Customer? old = null;
            if (request.CustomerID != 0)
            {
                // FIX: Changed .Customers to .Customer
                old = await _dbcontext.Customers.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            }

            if (old == null)
            {
                var model = _mapper.Map<Customer>(request);
                _dbcontext.Customers.Add(model);
                await _dbcontext.SaveChangesAsync();
                request.CustomerID = model.CustomerID;
            }
            else
            {
                old.CustomerCategory = request.CustomerCategory;
                old.CustomerType = request.CustomerType;
                old.CIDNumber = request.CIDNumber;
                // FIX: Changed .Customers to .Customer
                _dbcontext.Customers.Update(old);
            }
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
            // FIX: Changed .Customers to .Customer
            var model = await _dbcontext.Customers.FindAsync(customerId);
            if (model == null) throw new XNotAcceptableException("Customer record not found.");

            // FIX: Changed .Customers to .Customer
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