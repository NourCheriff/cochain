using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IProductLifeCycleService
    {
        public Task<List<ProductLifeCycleCategory>> GetCategories();
        public Task<List<ProductLifeCycle>> GetProductLifeCyclesByProductInfo(Guid productInfoId);
        public Task<ProductLifeCycle?> AddProductLifeCycle(ProductLifeCycle productLifeCycle);
        public Task<ProductLifeCycle?> AddProductLifeTransport(ProductLifeCycle productLifeCycle);
    }
}
