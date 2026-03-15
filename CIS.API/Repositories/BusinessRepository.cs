using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using CIS.Assets.Enum;
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

    public async Task UpsertAsync(BusinessInfoDTO.PageModel businessReq, AddressDTO.PageModel addressReq)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            if (businessReq.CustomerID == 0)
            {
                var customer = new Customer { EntityType = EntityType.Business, CreatedAt = DateTime.UtcNow };
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
                businessReq.CustomerID = customer.CustomerID;
            }

            var oldBiz = await _db.BusinessInfos.FirstOrDefaultAsync(e => e.CustomerID == businessReq.CustomerID);
            if (oldBiz == null)
            {
                var model = _mapper.Map<BusinessInfo>(businessReq);
                model.CustomerID = businessReq.CustomerID;
                _db.BusinessInfos.Add(model);
            }
            else
            {
                _mapper.Map(businessReq, oldBiz);
                _db.Entry(oldBiz).Property(x => x.BusinessInfoID).IsModified = false;
            }

            var oldAddr = await _db.Addresses.FirstOrDefaultAsync(e => e.EntityID == businessReq.CustomerID && e.EntityType == EntityType.Business);
            if (oldAddr == null)
            {
                var model = _mapper.Map<Address>(addressReq);
                model.EntityID = businessReq.CustomerID;
                model.EntityType = EntityType.Business;
                _db.Addresses.Add(model);
            }
            else
            {
                _mapper.Map(addressReq, oldAddr);
                _db.Entry(oldAddr).Property(x => x.AddressID).IsModified = false;
            }

            var oldContact = await _db.Contacts.FirstOrDefaultAsync(e => e.EntityID == businessReq.CustomerID && e.EntityType == EntityType.Business);
            if (oldContact == null)
            {
                var model = _mapper.Map<Contact>(businessReq);
                model.EntityID = businessReq.CustomerID;
                model.EntityType = EntityType.Business;
                _db.Contacts.Add(model);
            }
            else
            {
                _mapper.Map(businessReq, oldContact);
                _db.Entry(oldContact).Property(x => x.ContactID).IsModified = false;
            }

            var oldPurpose = await _db.AccountPurposes.FirstOrDefaultAsync(e => e.EntityID == businessReq.CustomerID && e.EntityType == EntityType.Business);
            if (oldPurpose == null)
            {
                var model = _mapper.Map<AccountPurpose>(businessReq);
                model.EntityID = businessReq.CustomerID;
                model.EntityType = EntityType.Business;
                _db.AccountPurposes.Add(model);
            }
            else
            {
                _mapper.Map(businessReq, oldPurpose);
                _db.Entry(oldPurpose).Property(x => x.AccountPurposeID).IsModified = false;
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
            var old = await _db.BusinessInterests
                .FirstOrDefaultAsync(e => e.BusinessInterestID == request.BusinessInterestID);

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