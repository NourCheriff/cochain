using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.CarbonOffset;
using Microsoft.AspNetCore.Http;
using CochainAPI.Model.Helper;
using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Utils;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class CarbonOffsettingActionRepository : SqlRepository, ICarbonOffsettingActionRepository
    {
        private readonly ILogRepository logRepository;
        public CarbonOffsettingActionRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<CarbonOffsettingAction> AddCarbonOffsettingAction(CarbonOffsettingAction action)
        {
            var savedAction = await dbContext.CarbonOffsettingAction.AddAsync(action);
            await dbContext.SaveChangesAsync();
            action.Id = savedAction.Entity.Id;
            var log = new Log()
            {
                Name = "Add CarbonOffsettingAction",
                Severity = "Info",
                Entity = "CarbonOffsettingAction",
                EntityId = action.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext.Request.Path,
                QueryString = httpContextAccessor.HttpContext.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return action;
        }

        public async Task<Page<CarbonOffsettingAction>> GetCarbonOffsettingActions(string? scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var query = dbContext.CarbonOffsettingAction.Where(x => (x.SupplyChainPartner != null && x.SupplyChainPartner.Name != null && (queryParam == null || x.SupplyChainPartner.Name.Contains(queryParam))) && (x.SupplyChainPartnerId != null && (scpId == null || x.SupplyChainPartnerId.ToString().Contains(scpId))));

            var totalSize = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value)
                .Take(pageSize.Value);
            }

            return new Page<CarbonOffsettingAction>
            {
                Items = await query.ToListAsync(),
                TotalSize = totalSize 
            };
        }

        public async Task<List<CarbonOffsettingAction>> GetOffsettingActionsToBeProcessed()
        {
            return await dbContext.CarbonOffsettingAction.Where(s => !s.IsProcessed).ToListAsync();
        }

        public async Task<bool> SaveCarbonOffsettingAction(CarbonOffsettingAction carbonOffsettingAction)
        {
            dbContext.CarbonOffsettingAction.Update(carbonOffsettingAction);
            var log = new Log()
            {
                Name = "Update CarbonOffsettingAction",
                Severity = "Info",
                Entity = "CarbonOffsettingAction",
                EntityId = carbonOffsettingAction.Id.ToString(),
                Action = "Update",
                UserId = "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}