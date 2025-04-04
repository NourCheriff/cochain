using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Helper;
using CochainAPI.Model.Product;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CochainAPI.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        public ProductService(IProductRepository productRepository, IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
        }

        public async Task<ProductInfo> AddProductInfo(ProductInfo productInfo)
        {
            var userId = _contextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userRepository.GetById(userId);
            productInfo.SupplyChainPartnerId = user!.SupplyChainPartnerId.GetValueOrDefault();
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

        public async Task<ProductInfo?> GetProductById(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out var productId))
            {
                return await _productRepository.GetProductById(productId);
            }

            return null;
        }

        public async Task<List<ProductInfo>?> GetIngredientsByProductInfoId(Guid id)
        {
            var productInfo = await GetProductById(id);

            if (productInfo != null)
            {
                Guid[] ingredientIds = productInfo.Ingredients.Select(ingredient => ingredient.IngredientId).ToArray();
                return await _productRepository.GetProductsByIds(ingredientIds);
            }

            return null;
        }

        public async Task<Page<ProductInfo>> GetProducts(string? queryParam, string? scpName, int? pageNumber, int? pageSize)
        {
            return await _productRepository.GetProducts(queryParam, scpName, pageNumber, pageSize);
        }

        public async Task<Page<ProductInfo>?> GetProductsOfSCP(Guid id, string? queryParam, int? pageNumber, int? pageSize)
        {
            if (Guid.TryParse(id.ToString(), out Guid scpId))
            {
                return await _productRepository.GetProductsOfSCP(scpId, queryParam, pageNumber, pageSize);
            }
            return null;
        }

        public async Task<ProductInfo?> UpdateProduct(ProductInfo productObj)
        {
            var userId = _contextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userRepository.GetById(userId);
            ProductInfo obj = productObj;

            if (productObj.SupplyChainPartnerId != user!.SupplyChainPartnerId.GetValueOrDefault())
                return null;

            bool isSuccess = false;
            if (!string.IsNullOrEmpty(productObj.Id.ToString()))
            {
                obj = await _productRepository.GetProductById(productObj.Id);
                if (obj != null)
                {
                    obj.Name = productObj.Name;
                    obj.ProductId = productObj.ProductId;
                    obj.ExpirationDate = productObj.ExpirationDate;
                    obj.Ingredients = productObj.Ingredients;
                    obj.TokenId = productObj.TokenId;
                    isSuccess = await _productRepository.UpdateProduct(obj);
                }
            }

            return isSuccess ? obj : null;
        }
    }
}