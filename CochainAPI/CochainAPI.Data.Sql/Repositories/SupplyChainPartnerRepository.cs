using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CompanyEntities;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace CochainAPI.Data.Sql.Repositories
{
    public class SupplyChainPartnerRepository : SqlRepository, ISupplyChainPartnerRepository
    {
        private readonly ILogRepository logRepository;
        public SupplyChainPartnerRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<SupplyChainPartner?> GetSupplyChainPartnerById(Guid id)
        {            
            return await dbContext.SupplyChainPartner.Include(x => x.SupplyChainPartnerType).FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public async Task<List<SupplyChainPartnerType>> GetTypes()
        {
            return await dbContext.SupplyChainPartnerType.ToListAsync();
        }

        public async Task<Page<SupplyChainPartner>> GetSupplyChainPartners(string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.SupplyChainPartner.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)));

            var size = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            query = query.Include(x => x.SupplyChainPartnerType);
            var items = await query.ToListAsync();

            return new Page<SupplyChainPartner>
            {
                Items = items,
                TotalSize = size
            };
        }

        public async Task<SupplyChainPartner?> AddSupplyChainPartner(SupplyChainPartner supplyChainPartner)
        {
            var savedSupplyChainPartner = await dbContext.SupplyChainPartner.AddAsync(supplyChainPartner);
            await dbContext.SaveChangesAsync();
            supplyChainPartner.Id = savedSupplyChainPartner.Entity.Id;
            var log = new Log()
            {
                Name = "Add SCP",
                Severity = "Info",
                Entity = "SupplyChainPartner",
                EntityId = supplyChainPartner.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value,
                Timestamp = DateTime.UtcNow,
                Message = ""
            };
            await logRepository.AddLog(log);
            return supplyChainPartner;
        }
        
        public async Task<bool> UpdateScpCredits(Guid scpId, float credits)
        {
            var res = await dbContext.SupplyChainPartner.Where(s => s.Id == scpId)
                                                    .ExecuteUpdateAsync(s => s.SetProperty(
                                                        scp => scp.Credits,
                                                        scp => scp.Credits + credits)) > 0;
            var log = new Log()
            {
                Name = "Update SCP credits",
                Severity = "Info",
                Entity = "SupplyChainPartner",
                EntityId = scpId.ToString(),
                Action = "Update",
                UserId = "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                Timestamp = DateTime.UtcNow,
                Message = $"Credits amount: {credits}"
            };
            await logRepository.AddLog(log);
            return res;
        }
    }
}
