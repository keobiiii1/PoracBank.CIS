using CIS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CIS.API.Data
{
    public interface ITransactionPolicy
    {
        Task<IDbContextTransaction?> BeginSqlTransaction(CISDbContext context);
    }

    public class DefaultTransactionPolicy : ITransactionPolicy
    {
        public async Task<IDbContextTransaction?> BeginSqlTransaction(CISDbContext context)
        {
            var isInMemory = context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            if (!isInMemory)
                return await context.Database.BeginTransactionAsync();
            return null;
        }
    }
}