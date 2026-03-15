using AutoMapper;
using CIS.API.Data;
using CIS.Assets.DTO;
using CIS.Assets.Enum;
using CIS.Assets.Models;
using Microsoft.EntityFrameworkCore;
using static CIS.Assets.DTO.IndividualInfoDTO.PageModel;

namespace CIS.API.Repositories;

public class IndividualRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<CISDbContext> _dbContextFactory;
    private readonly ITransactionPolicy _transactionPolicy;

    public IndividualRepository(
        IMapper mapper,
        IDbContextFactory<CISDbContext> dbContextFactory,
        ITransactionPolicy transactionPolicy)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _transactionPolicy = transactionPolicy;
    }

    public async Task UpsertInfoAsync(IndividualInfoDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            if (request.CustomerID == 0)
            {
                var newCustomer = new Customer
                {
                    EntityType = EntityType.Individual,
                    CustomerCategory = request.CustomerCategory,
                    CustomerType = request.CustomerType,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Customers.Add(newCustomer);
                await _db.SaveChangesAsync(); // SQL generates the ID here
                request.CustomerID = newCustomer.CustomerID;
            }

            // --- 1. Main Individual Info ---
            var info = await _db.IndividualInfos.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (info == null)
            {
                info = _mapper.Map<IndividualInfo>(request);
                info.CustomerID = request.CustomerID;
                _db.IndividualInfos.Add(info);
            }
            else
            {
                _mapper.Map(request, info);
                _db.IndividualInfos.Update(info);
            }

            // --- 2. Individual Foreigner (Step 2) ---
            var foreigner = await _db.IndividualForeigners.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (request.IsForeigner) // Check your DTO property name
            {
                if (foreigner == null)
                {
                    foreigner = _mapper.Map<IndividualForeigner>(request);
                    foreigner.CustomerID = request.CustomerID;
                    _db.IndividualForeigners.Add(foreigner);
                }
                else
                {
                    _mapper.Map(request, foreigner);
                    _db.Entry(foreigner).Property(x => x.ForeignerID).IsModified = false;
                    _db.IndividualForeigners.Update(foreigner);
                }
            }
            else if (foreigner != null)
            {
                _db.IndividualForeigners.Remove(foreigner);
            }

            // --- 3. Individual Family (Step 5) ---
            var family = await _db.IndividualFamilies.FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);
            if (family == null)
            {
                family = _mapper.Map<IndividualFamily>(request);
                family.CustomerID = request.CustomerID;
                _db.IndividualFamilies.Add(family);
            }
            else
            {
                _mapper.Map(request, family);
                _db.Entry(family).Property(x => x.FamilyID).IsModified = false;
                _db.IndividualFamilies.Update(family);
            }

            // --- 4. Account Purpose (Step 6) ---
            var acc = await _db.AccountPurposes.FirstOrDefaultAsync(e => e.EntityID == request.CustomerID);
            if (acc == null)
            {
                acc = new AccountPurpose { EntityID = request.CustomerID, EntityType = EntityType.Individual };
                _db.AccountPurposes.Add(acc);
            }
            acc.PurposeOfAccount = request.AccountPurpose;
            acc.PurposeOfAccountOther = request.AccountPurposeOther;

            // Map products based on your UI selection
            acc.ProductSavings = request.ProductsAvailed == "Savings";
            acc.ProductLoan = request.ProductsAvailed == "Loan";
            acc.ProductCurrent = request.ProductsAvailed == "Checking";
            acc.ProductOthers = request.ProductsAvailed == "Others";

            // --- 5. Government Relations (Step 6 - Dynamic List) ---
            var oldRels = await _db.GovernmentRelations.Where(x => x.CustomerID == request.CustomerID).ToListAsync();
            _db.GovernmentRelations.RemoveRange(oldRels);

            if (request.HasGovRelative && request.GovRelatives != null)
            {
                foreach (var relReq in request.GovRelatives)
                {
                    _db.GovernmentRelations.Add(new GovernmentRelation
                    {
                        CustomerID = request.CustomerID,
                        Name = relReq.Name,
                        Relationship = relReq.Relationship,
                        HighestPositionOccupied = relReq.Position,
                        PeriodCovered = relReq.PeriodCovered
                    });
                }
            }

            // --- 6. Business Interests (Step 6 - Dynamic List) ---
            var oldBiz = await _db.BusinessInterests.Where(x => x.CustomerID == request.CustomerID).ToListAsync();
            _db.BusinessInterests.RemoveRange(oldBiz);

            if (request.BusinessInterests != null)
            {
                foreach (var bizReq in request.BusinessInterests)
                {
                    _db.BusinessInterests.Add(new BusinessInterest
                    {
                        CustomerID = request.CustomerID,
                        BusinessName = bizReq.BusinessName,
                        OwnershipPercentage = bizReq.OwnershipPercentage
                    });
                }
            }

            await _db.SaveChangesAsync();
            if (tx != null) await tx.CommitAsync();
        }
        catch (Exception ex)
        {
            if (tx != null) await tx.RollbackAsync();
            throw; // Consider logging ex here
        }
    }

    // --- Step 3: Identification ---
    public async Task UpsertIdentificationAsync(IndividualIdentificationDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        var old = await _db.IndividualIdentifications
            .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

        if (old == null)
        {
            _db.IndividualIdentifications.Add(_mapper.Map<IndividualIdentification>(request));
        }
        else
        {
            _mapper.Map(request, old);
            _db.IndividualIdentifications.Update(old);
        }
        await _db.SaveChangesAsync();
    }

    // --- Step 4: Employment ---
    public async Task UpsertEmploymentAsync(IndividualEmploymentDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();

        if (request.CustomerID <= 0)
            throw new Exception("CustomerID is required for employment upsert.");

        var old = await _db.IndividualEmployments
            .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

        if (old == null)
        {
            _db.IndividualEmployments.Add(_mapper.Map<IndividualEmployment>(request));
        }
        else
        {
            _mapper.Map(request, old);
            _db.IndividualEmployments.Update(old);
        }

        await _db.SaveChangesAsync();
    }

    // --- Step 5: Family ---
    public async Task UpsertFamilyAsync(IndividualFamilyDTO.PageModel request)
    {
        using var _db = _dbContextFactory.CreateDbContext();
        using var tx = await _transactionPolicy.BeginSqlTransaction(_db);
        try
        {
            var old = await _db.IndividualFamilies
                .FirstOrDefaultAsync(e => e.CustomerID == request.CustomerID);

            if (old == null)
            {
                _db.IndividualFamilies.Add(_mapper.Map<IndividualFamily>(request));
            }
            else
            {
                _mapper.Map(request, old);
                _db.IndividualFamilies.Update(old);
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