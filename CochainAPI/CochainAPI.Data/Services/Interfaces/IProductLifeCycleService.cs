using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IProductLifeCycleService
    {
        public Task<List<ProductLifeCycleCategory>> GetCategories();
        public Task<List<ProductLifeCycle>> GetProductLifeCyclesByProductInfo(Guid productInfoId);
        public Task<List<ProductLifeCycle>> GetProductLifeCyclesToBeProcessed();
        public Task<ProductLifeCycle?> AddProductLifeCycle(ProductLifeCycle productLifeCycle);
        public Task<ProductLifeCycle?> AddProductLifeCycleTransport(ProductLifeCycle productLifeCycle);
        public Task<bool> SaveProductLife(ProductLifeCycle productLifeCycle);
    }
}
