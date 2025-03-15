using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Product;
using Microsoft.AspNetCore.Http;

namespace CochainAPI.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductService(IProductRepository productRepository, IHttpContextAccessor contextAccessor)
        {
            _productRepository = productRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ProductInfo> AddProductInfo(ProductInfo productInfo)
        {
            return await _productRepository.AddProductInfo(productInfo);
        }

        public async Task<List<ProductCategory>> GetCategories()
        {
            return await _productRepository.GetCategories();
        }

        public async Task<List<Product>?> GetGenericProducts(Guid id)
        {
            return await _productRepository.GetGenericProducts(id);
        }

        public async Task<List<ProductInfo>?> GetProductById(Guid id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<List<ProductInfo>> GetProducts(string? queryParam, string? scpName, int? pageNumber, int? pageSize)
        {
            return await _productRepository.GetProducts(queryParam, scpName, pageNumber, pageSize);
        }

        public async Task<List<ProductInfo>?> GetProductsOfSCP(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out Guid scpId))
            {
                return await _productRepository.GetProductsOfSCP(id);
            }
            return null;
        }
    }
}