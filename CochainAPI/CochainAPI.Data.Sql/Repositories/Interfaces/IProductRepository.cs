using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductCategory>> GetCategories();
        Task<List<ProductInfo>> GetProductsOfSCP(Guid id);
    }
}
