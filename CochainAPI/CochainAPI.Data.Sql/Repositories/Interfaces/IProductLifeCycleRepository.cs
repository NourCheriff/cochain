using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductLifeCycleRepository
    {
        Task<List<ProductLifeCycleCategory?>> GetCategories();
        public Task<List<ProductLifeCycle?>> GetProductLifeCyclesByProductInfo(Guid productInfoId);
        public Task<ProductLifeCycle>? AddProductLifeCycle(ProductLifeCycle productLifeCycle);
    }
}
