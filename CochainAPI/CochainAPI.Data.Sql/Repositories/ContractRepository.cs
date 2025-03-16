using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
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

        public async Task<List<Contract>> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Contract.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)) && x.UserEmitterId == userId);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Contract>> GetReceivedContracts(Guid scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Contract.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)) && x.SupplyChainPartnerReceiverId == scpId);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}
