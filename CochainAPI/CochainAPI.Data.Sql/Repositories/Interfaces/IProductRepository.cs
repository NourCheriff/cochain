using CochainAPI.Model.Helper;
using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductInfo> AddProductInfo(ProductInfo productInfo);
        Task<List<ProductCategory>> GetCategories();
        Task<List<Product>?> GetGenericProducts(Guid id);
        Task<ProductInfo?> GetProductById(Guid id);
        Task<List<ProductInfo>?> GetProductsByIds(Guid[] id);
        Task<bool> UpdateProduct(ProductInfo productObj);
        Task<Page<ProductInfo>?> GetProductsOfSCP(Guid id, string? queryParam, int? pageNumber, int? pageSize);
        Task<Page<ProductInfo>> GetProducts(string? productName, string? scpName, int? pageNumber, int? pageSize);
    }
}
