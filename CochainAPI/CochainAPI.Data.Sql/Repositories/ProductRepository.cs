using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Helper;
using CochainAPI.Model.Product;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductRepository : SqlRepository, IProductRepository
    {
        private readonly ILogRepository logRepository;
        public ProductRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<ProductInfo> AddProductInfo(ProductInfo productInfo)
        {
            var savedProductInfo = await dbContext.ProductInfo.AddAsync(productInfo);
            await dbContext.SaveChangesAsync();
            productInfo.Id = savedProductInfo.Entity.Id;
            var log = new Log()
            {
                Name = "Add Product Info",
                Severity = "Info",
                Entity = "ProductInfo",
                EntityId = productInfo.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return productInfo;
        }

        public async Task<List<ProductCategory>> GetCategories()
        {
            return await dbContext.ProductCategory.ToListAsync();
        }

        public async Task<List<Product>?> GetGenericProducts(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out Guid categoryId))
            {
                return await dbContext.Product.Where(x => x.CategoryId == categoryId)
                .ToListAsync();
            }
            var log = new Log()
            {
                Name = "Get Generic Products",
                Severity = "Warn",
                Entity = "Product",
                EntityId = id.ToString(),
                Action = "Read",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "The product category does not exist.",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);

            return null;
        }

        public async Task<Page<ProductInfo>> GetProducts(string? productName, string? scpName, int? pageNumber, int? pageSize)
        {
            var query = dbContext.ProductInfo.Include(x => x.SupplyChainPartner).Where(x => (productName == null || (x.Name != null && x.Name.Contains(productName))) && (scpName == null || (x.SupplyChainPartner != null && x.SupplyChainPartner.Name != null && x.SupplyChainPartner.Name.Contains(scpName))));

            var totalSize = await query.CountAsync();

            if (int.TryParse(pageSize?.ToString(), out int size) && int.TryParse(pageNumber?.ToString(), out int number))
            {
                query = query.Skip(size * number)
                .Take(size);
            }

            var queryComplete = query.Include(x => x.Ingredients)
                        .Include(x => x.Product)
                        .Include(x => x.Product!.Category)
                        .Include(x => x.ProductDocuments)
                        .Include(x => x.ProductLifeCycles!.AsQueryable())
                             .ThenInclude(y => y.ProductLifeCycleCategory);

            return new Page<ProductInfo>
            {
                Items = await queryComplete.ToListAsync(),
                TotalSize = totalSize
            };
        }

        public async Task<ProductInfo?> GetProductById(Guid id)
        {
            return await dbContext.ProductInfo.Where(x => x.Id == id)
                    .Include(x => x.Ingredients)
                    .Include(x => x.Product)
                    .Include(x => x.Product!.Category)
                    .Include(x => x.ProductDocuments)
                    .Include(x => x.ProductLifeCycles!.AsQueryable())
                             .ThenInclude(y => y.ProductLifeCycleCategory)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<ProductInfo>?> GetProductsByIds(Guid[] ids)
        {
            return await dbContext.ProductInfo.Where(x => ids.Contains(x.Id))
                    .Include(x => x.Ingredients)
                    .Include(x => x.Product)
                    .Include(x => x.ProductDocuments)
                    .Include(x => x.ProductLifeCycles!.AsQueryable())
                             .ThenInclude(y => y.ProductLifeCycleCategory)
                    .ToListAsync();
        }

        public async Task<Page<ProductInfo>?> GetProductsOfSCP(Guid id, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.ProductInfo.Where(x => x.SupplyChainPartnerId == id && x.Name != null && (queryParam == null || x.Name!.Contains(queryParam)));

            var totalSize = await query.CountAsync();

            if (int.TryParse(pageSize?.ToString(), out int size) && int.TryParse(pageNumber?.ToString(), out int number))
            {
                query = query.Skip(size * number)
                .Take(size);
            }

            var queryComplete = query.Include(x => x.Ingredients)
                                    .Include(x => x.Product)
                                    .Include(x => x.ProductLifeCycles)
                                    .Include(x => x.Product!.Category)
                                    .Include(x => x.ProductDocuments);

            return new Page<ProductInfo>
            {
                Items = await queryComplete.ToListAsync(),
                TotalSize = totalSize
            };
        }


        public async Task<bool> UpdateProduct(ProductInfo productObj)
        {
            dbContext.ProductInfo.Update(productObj);
            return await dbContext.SaveChangesAsync() > 0;
        }

    }
}