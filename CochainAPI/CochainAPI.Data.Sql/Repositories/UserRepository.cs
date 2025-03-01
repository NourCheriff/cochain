using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Model.Authentication;
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

        public async Task<IEnumerable<User>> GetAllActive()
        {
            return await dbContext.Users.Where(x => x.isActive == true).ToListAsync();
        }

        public async Task<User?> GetById(string id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<UserTemporaryPassword?> GetUserWithCredentials(AuthenticateRequest model)
        {
            return await dbContext.UserTemporaryPassword.SingleOrDefaultAsync(x => x.User.UserName == model.UserId && x.Password == model.Password && x.ExpirationDate >= DateTime.UtcNow && !x.IsUsed);
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
