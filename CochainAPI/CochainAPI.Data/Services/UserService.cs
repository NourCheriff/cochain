using Microsoft.Extensions.Options;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Authentication;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Data.Helpers;
using Microsoft.AspNetCore.Identity;

namespace CochainAPI.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly ISupplyChainPartnerRepository _supplyChainPartnerRepository;

        public UserService(IOptions<AppSettings> appSettings, IEmailService emailService, IUserRepository userRepository, ISupplyChainPartnerRepository supplyChainPartnerRepository)
        {
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _userRepository = userRepository;
            _supplyChainPartnerRepository = supplyChainPartnerRepository;
        }

        public async Task<List<User>> GetAllActive()
        {
            return await _userRepository.GetAllActive();
        }

        public async Task<User?> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task<User?> AddAndUpdateUser(User userObj)
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(userObj.Id))
            {
                var obj = await _userRepository.GetById(userObj.Id);
                if (obj != null)
                {
                    obj.FirstName = userObj.FirstName;
                    obj.LastName = userObj.LastName;
                    isSuccess = await _userRepository.UpdateUser(obj);                   
                }
            }
            else
            {
                if (Guid.TryParse(userObj.SupplyChainPartnerId.ToString(), out Guid scpId))
                {
                    var scp = await _supplyChainPartnerRepository.GetById(scpId);
                    if (scp != null)
                    {

                    }
                    var newUser = await _userRepository.AddUser(userObj);
                    isSuccess = newUser != null;
                    userObj = newUser ?? userObj;
                }                
            }

            return isSuccess ? userObj : null;

        }

        private bool ValidateUserInput(User user)
        {
            bool emailValid = !string.IsNullOrEmpty(user.UserName) && user.UserName.IsValidEmail();
            return emailValid; 
        }

        public async Task<bool> GenerateTemporaryPassword(AuthenticateRequest model)
        {
            var user = await _userRepository.GetByUserName(model.Username);
            if (user?.Id != null)
            {
                if (string.IsNullOrEmpty(user.UserName) || !user.UserName.IsValidEmail())
                {
                    return false;
                }
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
                await _userRepository.AddTemporaryPassword(temporaryPassword);
                _emailService.EmailPasswordTemporanea(user.UserName!, randomPassword);
                return true;
            }
            return false;
        }

        public async Task<User?> Authenticate(AuthenticateRequest model)
        {
            var userValid = await _userRepository.GetUserWithCredentials(model);
            if (userValid != null)
            {
                userValid.IsUsed = true;
                await _userRepository.UpdateTemporaryPassword(userValid);
                return userValid.User;
            }
            return null;
        }

        public async Task<List<IdentityRole>> GetRolesByUserId(string userId)
        {
            return await _userRepository.GetRolesByUserId(userId);
        }
    }
}
