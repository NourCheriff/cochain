using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Product;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductLifeCycleRepository : SqlRepository, IProductLifeCycleRepository
    {
        public ProductLifeCycleRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductLifeCycleCategory?>> GetCategories()
        {
            return await dbContext.ProductLifeCycleCategory.ToListAsync<ProductLifeCycleCategory?>();
        }
    }
}
