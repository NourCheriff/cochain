using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Product;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductLifeCycleRepository : SqlRepository, IProductLifeCycleRepository
    {
        public ProductLifeCycleRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductLifeCycle> AddProductLifeCycle(ProductLifeCycle productLifeCycle)
        {
            var savePLC = await dbContext.ProductLifeCycle.AddAsync(productLifeCycle);
            await dbContext.SaveChangesAsync();
            productLifeCycle.Id = savePLC.Entity.Id;
            return productLifeCycle;
        }

        public async Task<List<ProductLifeCycleCategory>> GetCategories()
        {
            return await dbContext.ProductLifeCycleCategory.ToListAsync();
        }

        public async Task<List<ProductLifeCycle>> GetProductLifeCyclesByProductInfo(Guid productInfoId)
        {
            return await dbContext.ProductLifeCycle.Where(x => x.ProductInfoId == productInfoId).ToListAsync();
        }
    }
}
