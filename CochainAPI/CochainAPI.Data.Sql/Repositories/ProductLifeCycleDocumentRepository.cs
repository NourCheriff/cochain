using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ProductLifeCycleDocumentRepository : SqlRepository, IProductLifeCycleDocumentRepository
    {
        private readonly ILogRepository logRepository;
        public ProductLifeCycleDocumentRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<ProductLifeCycleDocument?> AddDocument(ProductLifeCycleDocument documentObj)
        {
            var savedDocument = await dbContext.ProductLifeCycleDocument.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            var log = new Log()
            {
                Name = "Add product document",
                Severity = "Info",
                Entity = "ProductLifeCycleDocument",
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
            var productLifeCycleDocument = await dbContext.ProductLifeCycleDocument.FirstOrDefaultAsync(x => x.Id == id);
            if (productLifeCycleDocument != null)
            {
                dbContext.ProductLifeCycleDocument.Remove(productLifeCycleDocument);
                var res = await dbContext.SaveChangesAsync();
                log = new Log()
                {
                    Name = "Delete product document",
                    Severity = "Warn",
                    Entity = "ProductLifeCycleDocument",
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
                Entity = "ProductLifeCycleDocument",
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
            return await dbContext.ProductLifeCycleDocument.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }
    }
}
