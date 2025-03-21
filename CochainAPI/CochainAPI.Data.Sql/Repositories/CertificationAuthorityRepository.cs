using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Utils;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CertificationAuthorityRepository : SqlRepository, ICertificationAuthorityRepository
    {
        private readonly ILogRepository logRepository;
        public CertificationAuthorityRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        private async Task<SupplyChainPartnerCertificate?> Get(Guid documentId)
        {
            return await dbContext.SupplyChainPartnerCertificate.Where(x => x.Id == documentId).FirstOrDefaultAsync();
        }

        public async Task<List<SupplyChainPartnerCertificate>> GetSustainabilityCertificate(string certificationAuthorityId)
        {
            return await dbContext.SupplyChainPartnerCertificate.Where(x => x.UserEmitterId.Equals(certificationAuthorityId)).ToListAsync();
        }
        public async Task<bool> DeleteSustainabilityCertificate(Guid documentId)
        {
            Log log;
            SupplyChainPartnerCertificate? sustainabilityCertificate = dbContext.SupplyChainPartnerCertificate.SingleOrDefault(item => item.Id == documentId);

            if (sustainabilityCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Remove(sustainabilityCertificate);
                await dbContext.SaveChangesAsync();
                log = new Log()
                {
                    Name = "Delete Sustainability Certificate",
                    Severity = "Info",
                    Entity = "SupplyChainPartnerCertificate",
                    EntityId = documentId.ToString(),
                    Action = "Delete",
                    UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Timestamp = DateTime.UtcNow,
                    Message = "",
                    URL = httpContextAccessor.HttpContext?.Request.Path,
                    QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
                };
                await logRepository.AddLog(log);
                return true;
            }
            else
            {
                log = new Log()
                {
                    Name = "Delete Sustainability Certificate",
                    Severity = "Alert",
                    Entity = "SupplyChainPartnerCertificate",
                    EntityId = documentId.ToString(),
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
        }
        public async Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(Guid documentId)
        {
            Log log;
            SupplyChainPartnerCertificate? supplyChainPartnerCertificate = await Get(documentId);
            if (supplyChainPartnerCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Update(supplyChainPartnerCertificate);
                await dbContext.SaveChangesAsync();
                log = new Log()
                {
                    Name = "Update Sustainability Certificate",
                    Severity = "Info",
                    Entity = "SupplyChainPartnerCertificate",
                    EntityId = documentId.ToString(),
                    Action = "Update",
                    UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Timestamp = DateTime.UtcNow,
                    Message = "",
                    URL = httpContextAccessor.HttpContext?.Request.Path,
                    QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
                };
                await logRepository.AddLog(log);
                return await Get(documentId);
            }
            else
            {
                log = new Log()
                {
                    Name = "Update Sustainability Certificate",
                    Severity = "Alert",
                    Entity = "SupplyChainPartnerCertificate",
                    EntityId = documentId.ToString(),
                    Action = "Update",
                    UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Timestamp = DateTime.UtcNow,
                    Message = "Trying to update not existing document",
                    URL = httpContextAccessor.HttpContext?.Request.Path,
                    QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
                };
                await logRepository.AddLog(log);
                return null;
            }
        }

        public async Task<Page<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.CertificationAuthority.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)));

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<CertificationAuthority>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }

        public async Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id)
        {            
            return await dbContext.CertificationAuthority.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CertificationAuthority?> AddCertificationAuthority(CertificationAuthority certificationAuthority)
        {
            var savedCertificationAuthority = await dbContext.CertificationAuthority.AddAsync(certificationAuthority);
            await dbContext.SaveChangesAsync();
            certificationAuthority.Id = savedCertificationAuthority.Entity.Id;
            var log = new Log()
            {
                Name = "Add CA",
                Severity = "Info",
                Entity = "CertificationAuthority",
                EntityId = certificationAuthority.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return certificationAuthority;
        }
    }
}