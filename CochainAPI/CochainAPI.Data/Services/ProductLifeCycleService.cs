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

        public async Task<ProductLifeCycle?> AddProductLifeCycle(ProductLifeCycle productLifeCycle)
        {
            if (productLifeCycle.ProductLifeCycleCategory.Name!.Equals("Trasporto"))
                return null;

            return await _productLifeCycleRepository.AddProductLifeCycle(productLifeCycle);
        }

        public async Task<ProductLifeCycle?> AddProductLifeTransport(ProductLifeCycle productLifeCycle)
        {
            if (!productLifeCycle.ProductLifeCycleCategory.Name!.Equals("Trasporto"))
                return null;
            return await _productLifeCycleRepository.AddProductLifeCycle(productLifeCycle);
        }

        public async Task<List<ProductLifeCycleCategory>> GetCategories()
        {
            return await _productLifeCycleRepository.GetCategories();
        }

        public async Task<List<ProductLifeCycle>> GetProductLifeCyclesByProductInfo(Guid productInfoId)
        {
            if (Guid.TryParse(productInfoId.ToString(), out var id))
            {
                return await _productLifeCycleRepository.GetProductLifeCyclesByProductInfo(id);
            }

            return new List<ProductLifeCycle>();
        }

        public Task<List<ProductLifeCycle>> GetProductLifeCyclesToBeProcessed()
        {
            return _productLifeCycleRepository.GetProductLifeCyclesToBeProcessed();
        }

        public async Task<bool> SaveProductLife(ProductLifeCycle productLifeCycle)
        {
            return await _productLifeCycleRepository.SaveProductLife(productLifeCycle);
        }
    }
}