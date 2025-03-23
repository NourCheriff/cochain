using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductDocumentRepository : SqlRepository, IProductDocumentRepository
    {
        private readonly ILogRepository logRepository;
        private readonly IUserRepository userRepository;
        public ProductDocumentRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
            this.userRepository = userRepository;
        }

        public async Task<ProductDocument?> AddDocument(ProductDocument documentObj)
        {
            var userId = httpContextAccessor.HttpContext!.User.Claims.First( x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userRepository.GetById(userId);
            documentObj.SupplyChainPartnerReceiverId = user!.SupplyChainPartnerId.GetValueOrDefault();
            
            var savedDocument = await dbContext.ProductDocument.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            var log = new Log()
            {
                Name = "Add product document",
                Severity = "Info",
                Entity = "ProductDocument",
                EntityId = documentObj.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return documentObj;
        }

        public async Task<bool> DeleteDocumentById(Guid id)
        {
            Log log;
            var productDocument = await dbContext.ProductDocument.FirstOrDefaultAsync(x => x.Id == id);
            if (productDocument != null)
            {
                dbContext.ProductDocument.Remove(productDocument);
                var res = await dbContext.SaveChangesAsync();
                log = new Log()
                {
                    Name = "Delete product document",
                    Severity = "Warn",
                    Entity = "ProductDocument",
                    EntityId = id.ToString(),
                    Action = "Delete",
                    UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Timestamp = DateTime.UtcNow,
                    Message = "",
                    URL = httpContextAccessor.HttpContext?.Request.Path,
                    QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
                };
                await logRepository.AddLog(log);
                return res > 0;
            }
            log = new Log()
            {
                Name = "Delete product document",
                Severity = "Alert",
                Entity = "ProductDocument",
                EntityId = id.ToString(),
                Action = "Delete",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "Trying to delete not existing document",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return false;
        }

        public async Task<BaseDocument?> GetById(string id)
        {
            return await dbContext.ProductDocument.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }
    }
}
