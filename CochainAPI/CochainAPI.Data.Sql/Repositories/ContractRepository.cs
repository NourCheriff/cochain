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

        public async Task<Contract?> AddDocument(Contract documentObj)
        {
            var savedDocument = await dbContext.Contract.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            return documentObj;
        }

        public async Task<BaseDocument?> GetById(string id)
        {
            return await dbContext.Contract.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }
    }
}
