using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Utils;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class SupplyChainPartnerCertificateRepository : SqlRepository, ISupplyChainPartnerCertificateRepository
    {
        private readonly ILogRepository logRepository;
        public SupplyChainPartnerCertificateRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<SupplyChainPartnerCertificate?> AddDocument(SupplyChainPartnerCertificate documentObj)
        {
            var savedDocument = await dbContext.SupplyChainPartnerCertificate.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            var log = new Log()
            {
                Name = "Add sustainability certificate",
                Severity = "Info",
                Entity = "SupplyChainPartnerCertificate",
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
            var scpCertificate = await dbContext.SupplyChainPartnerCertificate.FirstOrDefaultAsync(x => x.Id == id);
            if (scpCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Remove(scpCertificate);
                var res = await dbContext.SaveChangesAsync();
                log = new Log()
                {
                    Name = "Delete sustainability certificate",
                    Severity = "Warn",
                    Entity = "SupplyChainPartnerCertificate",
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
                Name = "Delete sustainability certificate",
                Severity = "Alert",
                Entity = "SupplyChainPartnerCertificate",
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
            return await dbContext.SupplyChainPartnerCertificate.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }

        public async Task<Page<SupplyChainPartnerCertificate>> GetSustainabilityCertificates(string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.SupplyChainPartnerCertificate.Where(x => x.SupplyChainPartnerReceiver!.Name != null && (queryParam == null || x.SupplyChainPartnerReceiver.Name.Contains(queryParam)));

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<SupplyChainPartnerCertificate>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }
    }
}
