using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CertificationAuthorityRepository : SqlRepository, ICertificationAuthorityRepository
    {
        private readonly ILogRepository logRepository;
        public CertificationAuthorityRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
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
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value,
                Timestamp = DateTime.UtcNow,
                Message = ""
            };
            await logRepository.AddLog(log);
            return certificationAuthority;
        }

        public async Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id)
        {            
            return await dbContext.CertificationAuthority.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}