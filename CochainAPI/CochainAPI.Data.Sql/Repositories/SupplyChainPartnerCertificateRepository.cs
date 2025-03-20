using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;

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
