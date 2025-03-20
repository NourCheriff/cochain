using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Product;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductRepository : SqlRepository, IProductRepository
    {
        public ProductRepository(CochainDBContext dbContext) : base(dbContext)
        {
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

        public async Task<List<ProductInfo>> GetProducts(string? productName, string? scpName, int? pageNumber, int? pageSize)
        {
            var query = dbContext.ProductInfo.Include(x => x.SupplyChainPartner).Where(x =>  (productName == null || (x.Name != null && x.Name.Contains(productName))) && (scpName == null || (x.SupplyChainPartner != null && x.SupplyChainPartner.Name != null && x.SupplyChainPartner.Name.Contains(scpName))));

            if (int.TryParse(pageSize?.ToString(), out int size) && int.TryParse(pageNumber?.ToString(), out int number))
            {
                query = query.Skip(size * number)
                .Take(size);
            }

            var queryComplete = query.Include(x => x.Ingredients)
                        .Include(x => x.Product)
                        .Include(x => x.ProductDocuments)
                        .Include(x => x.Product!.Category)
                        .Include(x => x.ProductLifeCycles!.AsQueryable())
                             .ThenInclude(y => y.ProductLifeCycleCategory);

            
            return await queryComplete.ToListAsync();
        }

        public async Task<ProductInfo?> GetProductById(Guid id)
        {
            return await dbContext.ProductInfo.Where(x => x.Id == id)
                    .Include(x => x.Ingredients)
                    .Include(x => x.Product)
                    .Include(x => x.ProductLifeCycles)
                    .Include(x => x.ProductDocuments)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<ProductInfo>?> GetProductsByIds(Guid[] ids)
        {
            return await dbContext.ProductInfo.Where(x => ids.Contains(x.Id))
                    .Include(x => x.Ingredients)
                    .Include(x => x.Product)
                    .Include(x => x.ProductLifeCycles)
                    .Include(x => x.ProductDocuments)
                    .ToListAsync();
        }

        public async Task<List<ProductInfo>?> GetProductsOfSCP(Guid id, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.ProductInfo.Where(x => x.SupplyChainPartnerId == id && x.Name != null && (queryParam == null || x.Name!.Contains(queryParam)));

            if (int.TryParse(pageSize?.ToString(), out int size) && int.TryParse(pageNumber?.ToString(), out int number))
            {
                query = query.Skip(size * number)
                .Take(size);
            }

            var queryComplete = query.Include(x => x.Ingredients)
                                    .Include(x => x.Product)
                                    .Include(x => x.ProductLifeCycles)
                                    .Include(x => x.ProductDocuments);

            return await queryComplete.ToListAsync();
        }


        public async Task<bool> UpdateProduct(ProductInfo productObj)
        {
            dbContext.ProductInfo.Update(productObj);
            return await dbContext.SaveChangesAsync() > 0;
        }

    }
}