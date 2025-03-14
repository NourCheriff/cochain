using CochainAPI.Model.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllActive();
        Task<User?> GetById(string id);
        Task<List<User>?> GetUsersByCompanyId(Guid id);
        Task<User?> GetByUserName(string userName);
        Task<User?> AddUser(User userObj);
        Task<bool> UpdateUser(User userObj);
        Task<bool> AddTemporaryPassword(UserTemporaryPassword temporaryPassword);
        Task<UserTemporaryPassword?> GetUserWithCredentials(AuthenticateRequest model);
        Task<bool> UpdateTemporaryPassword(UserTemporaryPassword temporaryPassword);
        Task<List<IdentityRole>> GetRolesByUserId(string userId);
    }
}
