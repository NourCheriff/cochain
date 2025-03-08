using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Documents;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CertificationAuthorityRepository : SqlRepository, ICertificationAuthorityRepository
    {
        public CertificationAuthorityRepository(CochainDBContext dbContext) : base(dbContext)
        {
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

        public async Task<List<CertificationAuthority>> GetCertificationAuthorities(string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.CertificationAuthority.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)));

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            var queryComplete = query.Include(x => x.CompanyType);


            return await queryComplete.ToListAsync();
        }

        public async Task<CertificationAuthority?> GetCertificationAuthorityById(Guid id)
        {            
            return await dbContext.CertificationAuthority.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}