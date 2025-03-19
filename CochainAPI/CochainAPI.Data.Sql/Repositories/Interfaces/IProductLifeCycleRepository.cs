using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductLifeCycleRepository
    {
        Task<List<ProductLifeCycleCategory>> GetCategories();
        public Task<List<ProductLifeCycle>> GetProductLifeCyclesByProductInfo(Guid productInfoId);
        public Task<List<ProductLifeCycle>> GetProductLifeCyclesToBeProcessed();
        public Task<ProductLifeCycle> AddProductLifeCycle(ProductLifeCycle productLifeCycle);
        public Task<bool> SaveProductLife(ProductLifeCycle productLifeCycle);
    }
}