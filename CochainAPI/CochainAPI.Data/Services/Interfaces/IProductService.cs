using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductInfo> AddProductInfo(ProductInfo productInfo);
        Task<List<ProductCategory>> GetCategories();
        Task<List<ProductInfo>> GetProductsOfSCP(Guid id);
        Task<List<ProductInfo>> GetProducts(string? queryParam, int pageNumber, int pageSize);
    }
}
