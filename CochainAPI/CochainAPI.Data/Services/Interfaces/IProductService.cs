using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductCategory>> GetCategories();
        Task<List<ProductInfo>> GetProductsOfSCP(Guid id);
    }
}
