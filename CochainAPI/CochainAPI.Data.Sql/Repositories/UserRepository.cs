using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Authentication;
using CochainAPI.Model.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CochainAPI.Data.Sql.Repositories
{
    public class UserRepository : SqlRepository, IUserRepository
    {
        private readonly ILogRepository logRepository;
        public UserRepository(CochainDBContext dbContext, ILogRepository logRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            this.logRepository = logRepository;
        }

        public async Task<bool> AddTemporaryPassword(UserTemporaryPassword temporaryPassword)
        {
            var tempPW = await dbContext.UserTemporaryPassword.AddAsync(temporaryPassword);
            var log = new Log()
            {
                Name = "Add Temporary Password",
                Severity = "Info",
                Entity = "UserTemporaryPassword",
                EntityId = tempPW.Entity.Id.ToString(),
                Action = "Insert",
                UserId = tempPW.Entity.UserId,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User?> AddUser(User userObj)
        {
            var savedUser = await dbContext.Users.AddAsync(userObj);
            await dbContext.SaveChangesAsync();
            userObj.Id = savedUser.Entity.Id;
            var log = new Log()
            {
                Name = "Add User",
                Severity = "Info",
                Entity = "User",
                EntityId = userObj.Id.ToString(),
                Action = "Insert",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return userObj;
        }

        public async Task<List<User>> GetAllActive()
        {
            var res = await dbContext.Users.Where(x => x.IsActive == true).ToListAsync();
            var log = new Log()
            {
                Name = "Get All Active Users",
                Severity = "Info",
                Entity = "User",
                EntityId = "",
                Action = "Read",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return res;
        }

        public async Task<User?> GetById(string id)
        {
            var res = await dbContext.Users.FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
            var log = new Log()
            {
                Name = "Get User By Id",
                Severity = "Info",
                Entity = "User",
                EntityId = id,
                Action = "Read",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return res;
        }

        public async Task<List<User>?> GetUsersByCompanyId(Guid id, string companyType)
        {
            var log = new Log()
            {
                Name = "Get User By Company Id",
                Severity = "Info",
                Entity = "User",
                EntityId = id.ToString(),
                Action = "Read",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Timestamp = DateTime.UtcNow,
                Message = "The entity id refers to the company",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            if (companyType == "scp")
                return await dbContext.Users.Where(x => x.SupplyChainPartnerId == id && x.IsActive == true).ToListAsync();

            return await dbContext.Users.Where(x => x.CertificationAuthorityId == id && x.IsActive == true).ToListAsync();
        }

        public async Task<User?> GetByUserName(string userName)
        {
            var res = await dbContext.Users.FirstOrDefaultAsync(c => c.UserName == userName && c.IsActive == true);
            var log = new Log()
            {
                Name = "Get User By Id",
                Severity = "Info",
                Entity = "User",
                EntityId = userName,
                Action = "Read",
                UserId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "ad00648b-a031-432d-b007-6a0829cf5292",
                Timestamp = DateTime.UtcNow,
                Message = "The entity id field refers to the user name",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return res;
        }

        public async Task<List<IdentityRole>> GetRolesByUserId(string userId)
        {
            var userRoles = await dbContext.UserRoles.Where(x => x.UserId == userId).ToListAsync();
            var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();
            return await dbContext.Roles.Where(x => roleIds.Contains(x.Id))
                                        .ToListAsync();
        }

        public async Task<UserTemporaryPassword?> GetUserWithCredentials(AuthenticateRequest model)
        {
            Log log;
            var tempPw = await dbContext.UserTemporaryPassword.Include(x => x.User).Where(x => x.User.UserName!.ToLower().Equals(model.Username.ToLower()) &&
                x.ExpirationDate >= DateTime.UtcNow &&
                !x.IsUsed).FirstOrDefaultAsync();
            if (tempPw != null)
            {                
                tempPw.Attempts++;
                tempPw.IsUsed = tempPw.Attempts > 3;
                dbContext.UserTemporaryPassword.Update(tempPw);
                if (tempPw.Password == model.Password && !tempPw.IsUsed)
                {
                    log = new Log()
                    {
                        Name = "Get User By Email And Password",
                        Severity = "Info",
                        Entity = "UserTemporaryPassword",
                        EntityId = model.Username,
                        Action = "Read",
                        UserId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "ad00648b-a031-432d-b007-6a0829cf5292",
                        Timestamp = DateTime.UtcNow,
                        Message = "The entity id refers to the username used",
                        URL = httpContextAccessor.HttpContext?.Request.Path,
                        QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
                    };
                    await logRepository.AddLog(log);
                    return tempPw;
                }
            }
            log = new Log()
            {
                Name = "Get User By Email And Password",
                Severity = "Alert",
                Entity = "UserTemporaryPassword",
                EntityId = model.Username,
                Action = "Read",
                UserId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "ad00648b-a031-432d-b007-6a0829cf5292",
                Timestamp = DateTime.UtcNow,
                Message = "The entity id refers to the username used",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return null;
        }

        public async Task<bool> UpdateTemporaryPassword(UserTemporaryPassword temporaryPassword)
        {
            dbContext.UserTemporaryPassword.Update(temporaryPassword);
            var log = new Log()
            {
                Name = "Update Temporary Password",
                Severity = "Info",
                Entity = "UserTemporaryPassword",
                EntityId = temporaryPassword.Id.ToString(),
                Action = "Update",
                UserId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "ad00648b-a031-432d-b007-6a0829cf5292",
                Timestamp = DateTime.UtcNow,
                Message = "",
                URL = httpContextAccessor.HttpContext?.Request.Path,
                QueryString = httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            };
            await logRepository.AddLog(log);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User userObj)
        {
            dbContext.Users.Update(userObj);
            var log = new Log()
            {
                Name = "Update User",
                Severity = "Info",
                Entity = "User",
                EntityId = userObj.Id.ToString(),
                Action = "Update",
                UserId = httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
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