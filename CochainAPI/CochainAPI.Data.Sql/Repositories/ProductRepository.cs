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

        public async Task<List<ProductCategory>> GetCategories()
        {
            return await dbContext.ProductCategory.ToListAsync();
        }

        public async Task<List<ProductInfo>> GetProductsOfSCP(Guid id)
        {
            return await dbContext.ProductInfo.Where(x => x.SupplyChainPartnerId == id).Include(x => x.Ingredients).Include(x => x.Product).Include(x => x.ProductLifeCycles).Include(x => x.ProductDocuments).ToListAsync();
        }
    }
}
