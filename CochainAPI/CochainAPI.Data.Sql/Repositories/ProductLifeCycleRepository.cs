using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Product;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            var log = new Log()
            {
                Name = "Add Product Life Cycle",
                Severity = "Info",
                Entity = "ProductLifeCycle",
                EntityId = productLifeCycle.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
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
            var log = new Log()
            {
                Name = "Update Product Life Cycle",
                Severity = "Info",
                Entity = "ProductLifeCycle",
                EntityId = productLifeCycle.Id.ToString(),
                Action = "Update",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
