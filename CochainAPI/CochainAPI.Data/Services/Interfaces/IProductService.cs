using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductCategory?>> GetCategories();
    }
}
