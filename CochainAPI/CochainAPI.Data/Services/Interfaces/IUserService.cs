using CochainAPI.Model.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> GenerateTemporaryPassword(AuthenticateRequest model);
        Task<User?> Authenticate(AuthenticateRequest model);
        Task<List<User>> GetAllActive();
        Task<User?> GetById(string id);
        Task<List<User>?> GetUsersByCompanyId(Guid id);
        Task<User?> AddUser(User userObj);
        Task<User?> UpdateUser(User userObj);
        Task<List<IdentityRole>> GetRolesByUserId(string userId);
    }
}
