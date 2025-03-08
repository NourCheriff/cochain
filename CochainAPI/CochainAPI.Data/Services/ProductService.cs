using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Product;

namespace CochainAPI.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductInfo> AddProductInfo(ProductInfo productInfo)
        {
            return await _productRepository.AddProductInfo(productInfo);
        }

        public async Task<List<ProductCategory>> GetCategories()
        {
            return await _productRepository.GetCategories();
        }

        public async Task<List<ProductInfo>> GetProducts(string? queryParam, int? pageNumber, int? pageSize)
        {
            return await _productRepository.GetProducts(queryParam, pageNumber, pageSize);
        }

        public async Task<List<ProductInfo>?> GetProductsOfSCP(Guid id)
        {
            return await _productRepository.GetProductsOfSCP(id);
        }
    }
}