using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class SupplyChainPartnerCertificateRepository : SqlRepository, ISupplyChainPartnerCertificateRepository
    {
        public SupplyChainPartnerCertificateRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<SupplyChainPartnerCertificate?> AddDocument(SupplyChainPartnerCertificate documentObj)
        {
            var savedDocument = await dbContext.SupplyChainPartnerCertificate.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            return documentObj;
        }

        public async Task<bool> DeleteDocumentById(Guid id)
        {
            var scpCertificate = await dbContext.SupplyChainPartnerCertificate.FirstOrDefaultAsync(x => x.Id == id);
            if (scpCertificate != null)
            {
                dbContext.SupplyChainPartnerCertificate.Remove(scpCertificate);
                var res = await dbContext.SaveChangesAsync();
                return res > 0;
            }
            return false;
        }

        public async Task<BaseDocument?> GetById(string id)
        {
            return await dbContext.SupplyChainPartnerCertificate.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }
    }
}
