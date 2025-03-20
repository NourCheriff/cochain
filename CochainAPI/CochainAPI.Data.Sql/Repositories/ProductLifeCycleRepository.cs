using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductLifeCycleRepository : SqlRepository, IProductLifeCycleRepository
    {
        private readonly ILogRepository logRepository;
        public ProductLifeCycleRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
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

        public async Task<List<ProductLifeCycle>> GetProductLifeCyclesToBeProcessed()
        {
            return await dbContext.ProductLifeCycle.Where(x => !x.IsEmissionProcessed).Include(x => x.SupplyChainPartner).ThenInclude(x => x.SupplyChainPartnerType).ToListAsync();
        }

        public async Task<bool> SaveProductLife(ProductLifeCycle productLifeCycle)
        {
            dbContext.ProductLifeCycle.Update(productLifeCycle);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
