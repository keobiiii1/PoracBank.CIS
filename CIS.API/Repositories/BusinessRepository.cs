using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;
using CIS.Assets.Enum;

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
            var oldBusiness = await _db.BusinessInfos.FirstOrDefaultAsync(e => e.BusinessInfoID == businessReq.BusinessInfoID);
            if (oldBusiness == null)
            {
                var model = _mapper.Map<BusinessInfo>(businessReq);
                _db.BusinessInfos.Add(model);
            }
            else
            {
                _mapper.Map(businessReq, oldBusiness);
                _db.BusinessInfos.Update(oldBusiness);
            }

            var oldAddress = await _db.Addresses.FirstOrDefaultAsync(e =>
                e.EntityID == businessReq.CustomerID &&
                e.EntityType == EntityType.Business);

            if (oldAddress == null)
            {
                var addrModel = _mapper.Map<Address>(addressReq);
                addrModel.EntityID = businessReq.CustomerID;
                addrModel.EntityType = EntityType.Business;
                _db.Addresses.Add(addrModel);
            }
            else
            {
                _mapper.Map(addressReq, oldAddress);
                _db.Addresses.Update(oldAddress);
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