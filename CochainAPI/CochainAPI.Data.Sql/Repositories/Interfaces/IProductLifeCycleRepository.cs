using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductLifeCycleRepository
    {
        Task<List<ProductLifeCycleCategory>> GetCategories();
        Task<ProductLifeCycleCategory?> GetCategoryByName(string name);
        Task<List<ProductLifeCycle>> GetProductLifeCyclesByProductInfo(Guid productInfoId);
        Task<List<ProductLifeCycle>> GetProductLifeCyclesToBeProcessed();
        Task<ProductLifeCycle> AddProductLifeCycle(ProductLifeCycle productLifeCycle);
        Task<bool> SaveProductLife(ProductLifeCycle productLifeCycle);
    }
}