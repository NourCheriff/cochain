using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ContractRepository : SqlRepository, IContractRepository
    {
        public ContractRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Contract> AddDocument(Contract documentObj)
        {
            var savedDocument = await dbContext.Contract.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            return documentObj;
        }

        public async Task<bool> DeleteDocumentById(Guid id)
        {
            var contract = await dbContext.Contract.FirstOrDefaultAsync(x => x.Id == id);
            if (contract != null)
            {
                dbContext.Contract.Remove(contract);
                var res = await dbContext.SaveChangesAsync();
                return res > 0;
            }
            return false;
        }

        public async Task<BaseDocument?> GetById(string id)
        {
            return await dbContext.Contract.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }

        public async Task<Page<Contract>> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Contract.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)) && x.UserEmitterId == userId);

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<Contract>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }

        public async Task<Page<Contract>> GetReceivedContracts(Guid scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Contract.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)) && x.SupplyChainPartnerReceiverId == scpId);

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<Contract>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }
    }
}
