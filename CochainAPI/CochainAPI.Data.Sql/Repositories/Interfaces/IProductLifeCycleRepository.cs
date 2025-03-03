using CochainAPI.Model.Product;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductLifeCycleRepository
    {
        Task<List<ProductLifeCycleCategory?>> GetCategories();
    }
}
