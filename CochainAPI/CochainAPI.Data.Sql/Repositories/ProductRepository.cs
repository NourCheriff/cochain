using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Product;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductRepository : SqlRepository, IProductRepository
    {
        public ProductRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductCategory?>> GetCategories()
        {
            return await dbContext.ProductCategory.ToListAsync<ProductCategory?>();
        }
    }
}
