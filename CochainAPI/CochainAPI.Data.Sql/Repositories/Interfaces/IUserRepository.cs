using CochainAPI.Model.Authentication;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllActive();
        Task<User?> GetById(string id);
        Task<User?> GetByUserName(string userName);
        Task<User?> AddUser(User userObj);
        Task<bool> UpdateUser(User userObj);
        Task<bool> AddTemporaryPassword(UserTemporaryPassword temporaryPassword);
        Task<UserTemporaryPassword?> GetUserWithCredentials(AuthenticateRequest model);
        Task<bool> UpdateTemporaryPassword(UserTemporaryPassword temporaryPassword);
    }
}
