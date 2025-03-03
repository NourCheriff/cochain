using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IProductLifeCycleService
    {
        public Task<List<ProductLifeCycleCategory?>> GetCategories();
    }
}
