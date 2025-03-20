using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            SupplyChainPartnerCertificate? sustainabilityCertificate = dbContext.SupplyChainPartnerCertificate.SingleOrDefault(item => item.Id == documentId);

            if (sustainabilityCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Remove(sustainabilityCertificate);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<SupplyChainPartnerCertificate?> UpdateSustainabilityCertificate(Guid documentId)
        {
            SupplyChainPartnerCertificate? supplyChainPartnerCertificate = await Get(documentId);
            if (supplyChainPartnerCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Update(supplyChainPartnerCertificate);
                await dbContext.SaveChangesAsync();
                return await Get(documentId);
            }
            else
            {
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
            return certificationAuthority;
        }
    }
}