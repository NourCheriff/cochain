using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductInfo> AddProductInfo(ProductInfo productInfo);
        Task<List<ProductCategory>> GetCategories();
        Task<List<Product>?> GetGenericProducts(Guid id);
        Task<List<ProductInfo>?> GetProductsOfSCP(Guid id);
        Task<List<ProductInfo>> GetProducts(string? productName, string? scpName, int? pageNumber, int? pageSize);
    }
}
