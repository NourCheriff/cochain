using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Helper;
using CochainAPI.Model.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

            return null;
        }

        public async Task<Page<ProductInfo>> GetProducts(string? productName, string? scpName, int? pageNumber, int? pageSize)
        {
            var query = dbContext.ProductInfo.Include(x => x.SupplyChainPartner).Where(x =>  (productName == null || (x.Name != null && x.Name.Contains(productName))) && (scpName == null || (x.SupplyChainPartner != null && x.SupplyChainPartner.Name != null && x.SupplyChainPartner.Name.Contains(scpName))));

            var totalSize = await query.CountAsync();

            if (int.TryParse(pageSize?.ToString(), out int size) && int.TryParse(pageNumber?.ToString(), out int number))
            {
                query = query.Skip(size * number)
                .Take(size);
            }

            var queryComplete = query.Include(x => x.Ingredients)
                        .Include(x => x.Product)
                        .Include(x => x.ProductLifeCycles)
                        .Include(x => x.ProductDocuments)
                        .Include(x => x.Product!.Category);

            return new Page<ProductInfo>
            {
                Items = await queryComplete.ToListAsync(),
                TotalSize = totalSize
            };
        }

        public async Task<List<ProductInfo>?> GetProductById(Guid id)
        {
            return await dbContext.ProductInfo.Where(x => x.Id == id)
                    .Include(x => x.Ingredients)
                    .Include(x => x.Product)
                    .Include(x => x.ProductLifeCycles)
                    .Include(x => x.ProductDocuments)
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
    }
}