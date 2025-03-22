using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace CochainAPI.Data.Sql.Repositories
{
    public class ContractRepository : SqlRepository, IContractRepository
    {
        private readonly ILogRepository logRepository;
        public ContractRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<Contract> AddDocument(Contract documentObj)
        {
            var savedDocument = await dbContext.Contract.AddAsync(documentObj);
            await dbContext.SaveChangesAsync();
            documentObj.Id = savedDocument.Entity.Id;
            var log = new Log()
            {
                Name = "Add contract",
                Severity = "Info",
                Entity = "Contract",
                EntityId = documentObj.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value,
                Timestamp = DateTime.UtcNow,
                Message = ""
            };
            await logRepository.AddLog(log);
            return documentObj;
        }

        public async Task<bool> DeleteDocumentById(Guid id)
        {
            Log log;
            var contract = await dbContext.Contract.FirstOrDefaultAsync(x => x.Id == id);
            if (contract != null)
            {
                dbContext.Contract.Remove(contract);
                var res = await dbContext.SaveChangesAsync();
                log = new Log()
                {
                    Name = "Delete contract",
                    Severity = "Warn",
                    Entity = "Contract",
                    EntityId = id.ToString(),
                    Action = "Delete",
                    UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value,
                    Timestamp = DateTime.UtcNow,
                    Message = ""
                };
                await logRepository.AddLog(log);
                return res > 0;
            }
            log = new Log()
            {
                Name = "Delete contract",
                Severity = "Alert",
                Entity = "Contract",
                EntityId = id.ToString(),
                Action = "Delete",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value,
                Timestamp = DateTime.UtcNow,
                Message = "Trying to delete not existing document"
            };
            await logRepository.AddLog(log);
            return false;
        }

        public async Task<BaseDocument?> GetById(string id)
        {
            return await dbContext.Contract.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }

        public async Task<Page<Contract>> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Contract.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)) && x.UserEmitterId == userId);

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<Contract>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }

        public async Task<Page<Contract>> GetReceivedContracts(Guid scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.Contract.Where(x => x.Name != null && (queryParam == null || x.Name.Contains(queryParam)) && x.SupplyChainPartnerReceiverId == scpId);

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<Contract>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }
    }
}
