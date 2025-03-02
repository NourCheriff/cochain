using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductLifeCycleDocumentRepository : SqlRepository, IProductLifeCycleDocumentRepository
    {
        public ProductLifeCycleDocumentRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductLifeCycleDocument?> AddDocument(ProductLifeCycleDocument documentObj)
        {
            var savedDocument = await dbContext.ProductLifeCycleDocument.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            return documentObj;
        }

        public async Task<BaseDocument?> GetById(string id)
        {
            return await dbContext.ProductLifeCycleDocument.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }
    }
}
