using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services
{
    public class ProductLifeCycleService : IProductLifeCycleService
    {
        private readonly IProductLifeCycleRepository _productLifeCycleRepository;
        public ProductLifeCycleService(IProductLifeCycleRepository productLifeCycleRepository)
        {
            _productLifeCycleRepository = productLifeCycleRepository;
        }
        public async Task<List<ProductLifeCycleCategory?>> GetCategories()
        {
            return await _productLifeCycleRepository.GetCategories();
        }
    }
}