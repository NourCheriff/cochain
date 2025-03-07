using System.Security.Cryptography.X509Certificates;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CertificationAuthorityRepository : SqlRepository, ICertificationAuthorityRepository
    {
        public CertificationAuthorityRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        private async Task<SupplyChainPartnerCertificate?> Get(Guid documentId)
        {
            return (SupplyChainPartnerCertificate?)dbContext.SupplyChainPartnerCertificate.Where(x => x.Id == documentId);
        }

        public async Task<IEnumerable<SupplyChainPartnerCertificate?>> GetSustainabilityCertificate(string certificationAuthorityId)
        {
            return await dbContext.SupplyChainPartnerCertificate.Where(x => x.UserEmitterId.Equals(certificationAuthorityId)).ToListAsync();
        }
        public async Task<Guid?> DeleteSustainabilityCertificate(Guid documentId)
        {
            SupplyChainPartnerCertificate? sustainabilityCertificate = dbContext.SupplyChainPartnerCertificate.SingleOrDefault(item => item.Id == documentId);

            if (sustainabilityCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Remove(sustainabilityCertificate);
                await dbContext.SaveChangesAsync();
                return documentId;
            }
            else
            {
                return null;
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
    }
}
