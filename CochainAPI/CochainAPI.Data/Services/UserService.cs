using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Data.Sql;
using CochainAPI.Model.Authentication;
using CochainAPI.Model.DTOs;

namespace CochainAPI.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly CochainDBContext db;
        private readonly IEmailService _emailService;

        public UserService(IOptions<AppSettings> appSettings, CochainDBContext _db, IEmailService emailService)
        {
            _appSettings = appSettings.Value;
            db = _db;
            _emailService = emailService;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.Where(x => x.isActive == true).ToListAsync();
        }

        public async Task<User?> GetById(string id)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User?> AddAndUpdateUser(User userObj)
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(userObj.Id))
            {
                var obj = await db.Users.FirstOrDefaultAsync(c => c.Id == userObj.Id);
                if (obj != null)
                {
                    // obj.Address = userObj.Address;
                    obj.FirstName = userObj.FirstName;
                    obj.LastName = userObj.LastName;
                    db.Users.Update(obj);
                    isSuccess = await db.SaveChangesAsync() > 0;
                }
            }
            else
            {
                await db.Users.AddAsync(userObj);
                isSuccess = await db.SaveChangesAsync() > 0;
            }

            return isSuccess ? userObj : null;

        }

        public async Task<bool> GenerateTemporaryPassword(AuthenticateRequest model)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == model.Username);
            if (user?.Id != null)
            {
                Random random = new Random();
                int randomNumber = random.Next(100000, 1000000);
                var randomPassword = $"{randomNumber}";
                var temporaryPassword = new UserTemporaryPassword()
                {
                    User = user,
                    Password = randomPassword,
                    ExpirationDate = DateTime.UtcNow.AddHours(2),
                    IsUsed = false
                };
                await db.UserTemporaryPassword.AddAsync(temporaryPassword);
                _emailService.EmailPasswordTemporanea(user.UserName!, randomPassword);
                return true;
            }
            return false;
        }

        public async Task<User?> Authenticate(AuthenticateRequest model)
        {
            var userValid = await db.UserTemporaryPassword.SingleOrDefaultAsync(x => x.User.UserName == model.UserId && x.Password == model.Password && x.ExpirationDate >= DateTime.UtcNow && !x.IsUsed);
            if (userValid != null)
            {
                userValid.IsUsed = true;
                db.UserTemporaryPassword.Update(userValid);
                await db.SaveChangesAsync();
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == model.UserId);
                return user;
            }
            return null;
        }
    }
}
