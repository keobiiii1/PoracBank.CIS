using AutoMapper;
using CIS.API.Data;
using CIS.Assets.Models;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

namespace CIS.API.Repositories;

public class IndividualRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public IndividualRepository(IMapper mapper, IDbContextFactory<CISDbContext> dbContextFactory, ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    // --- Step 2: Personal Information & Foreigner Details ---
    // CIS.API/Repositories/IndividualRepository.cs

    // CIS.API/Repositories/IndividualRepository.cs

    public async Task UpsertInfoAsync(IndividualInfoDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            // 1. SAVE TO cis.IndividualInfo (Primary Data)
            var info = await _db.IndividualInfos
                .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

            if (info == null)
            {
                info = _mapper.Map<IndividualInfo>(request);
                _db.IndividualInfos.Add(info);
            }
            else
            {
                _mapper.Map(request, info);
                _db.IndividualInfos.Update(info);
            }

            // 2. SAVE TO cis.IndividualForeigner (Conditional)
            if (request.IsForeigner)
            {
                var foreigner = await _db.IndividualForeigners
                    .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

                if (foreigner == null)
                {
                    foreigner = _mapper.Map<IndividualForeigner>(request);
                    foreigner.CustomerID = request.CustomerID;
                    _db.IndividualForeigners.Add(foreigner);
                }
                else
                {
                    _mapper.Map(request, foreigner);
                    _db.IndividualForeigners.Update(foreigner);
                }
            }

            // 3. SAVE TO cis.IndividualFamily (Spouse & Mother)
            var family = await _db.IndividualFamilies
                .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

            if (family == null)
            {
                family = new IndividualFamily { CustomerID = request.CustomerID };
                _db.IndividualFamilies.Add(family);
            }

            // Map the separated Spouse fields
            family.SpouseGivenName = request.SpouseGivenName;
            family.SpouseMiddleName = request.SpouseMiddleName;
            family.SpouseLastName = request.SpouseLastName;

            // Map the separated Mother fields
            family.MotherMaidenGivenName = request.MotherMaidenGivenName;
            family.MotherMaidenMiddleName = request.MotherMaidenMiddleName;
            family.MotherMaidenLastName = request.MotherMaidenLastName;

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch (Exception ex)
        {
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
    }

    // --- Step 4: Employment ---
    public async Task UpsertEmploymentAsync(IndividualEmploymentDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.IndividualEmployments
                .FirstOrDefaultAsync(e => e.EmploymentID == request.EmploymentID);

            if (old == null)
            {
                var model = _mapper.Map<IndividualEmployment>(request);
                _db.IndividualEmployments.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
                _db.IndividualEmployments.Update(old);
            }

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }

    // --- Step 5: Family ---
    public async Task UpsertFamilyAsync(IndividualFamilyDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.IndividualFamilies
                .FirstOrDefaultAsync(e => e.FamilyID == request.FamilyID);

            if (old == null)
            {
                var model = _mapper.Map<IndividualFamily>(request);
                _db.IndividualFamilies.Add(model);
            }
            else
            {
                _mapper.Map(request, old);
                _db.IndividualFamilies.Update(old);
            }

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch { if (tx != null) await tx.RollbackAsync(); throw; }
    }
}