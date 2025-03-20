using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Data.Sql.Repositories
{
    public class UserRepository : SqlRepository, IUserRepository
    {
        public UserRepository(CochainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddTemporaryPassword(UserTemporaryPassword temporaryPassword)
        {
            await dbContext.UserTemporaryPassword.AddAsync(temporaryPassword);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User?> AddUser(User userObj)
        {
            var savedUser = await dbContext.Users.AddAsync(userObj);
            await dbContext.SaveChangesAsync();
            userObj.Id = savedUser.Entity.Id;
            return userObj;
        }

        public async Task<List<User>> GetAllActive()
        {
            return await dbContext.Users.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<User?> GetById(string id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<User>?> GetUsersByCompanyId(Guid id)
        {
            if (Guid.TryParse(id.ToString(), out Guid companyId))
            {
            return await dbContext.Users.Where(x => x.SupplyChainPartnerId == companyId).ToListAsync();
            }
            return null;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(c => c.UserName == userName);
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
                    return tempPw;
                }
            }
            return null;
        }

        public async Task<bool> UpdateTemporaryPassword(UserTemporaryPassword temporaryPassword)
        {
            dbContext.UserTemporaryPassword.Update(temporaryPassword);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User userObj)
        {
            dbContext.Users.Update(userObj);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}