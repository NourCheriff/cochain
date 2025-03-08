using CochainAPI.Model.Authentication;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> GenerateTemporaryPassword(AuthenticateRequest model);
        Task<User?> Authenticate(AuthenticateRequest model);
        Task<List<User>> GetAllActive();
        Task<User?> GetById(string id);
        Task<User?> AddAndUpdateUser(User userObj);
    }
}
