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
        private readonly ICertificationAuthorityRepository _certificationAuthorityRepository;

        public UserService(IOptions<AppSettings> appSettings, IEmailService emailService, IUserRepository userRepository, ISupplyChainPartnerRepository supplyChainPartnerRepository, ICertificationAuthorityRepository certificationAuthorityRepository)
        {
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _userRepository = userRepository;
            _supplyChainPartnerRepository = supplyChainPartnerRepository;
            _certificationAuthorityRepository = certificationAuthorityRepository;
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
                if (!ValidateUserInput(userObj))
                    return null;

                var isSCP = !string.IsNullOrEmpty(userObj.SupplyChainPartnerId?.ToString());
                var isCA = !string.IsNullOrEmpty(userObj.CertificationAuthorityId?.ToString());
                if (!isSCP && !isCA)
                    return null;                

                if (isSCP && Guid.TryParse(userObj.SupplyChainPartnerId.ToString(), out Guid scpId))
                {
                    var scp = await _supplyChainPartnerRepository.GetSupplyChainPartnerById(scpId);
                    if (scp != null)
                    {
                        var newUser = await _userRepository.AddUser(userObj);
                        isSuccess = newUser != null;
                        userObj = newUser ?? userObj;
                    }                    
                }
                else if (isCA && Guid.TryParse(userObj.CertificationAuthorityId.ToString(), out Guid caId))
                {
                    var ca = await _certificationAuthorityRepository.GetCertificationAuthorityById(caId);
                    if (ca != null)
                    {
                        var newUser = await _userRepository.AddUser(userObj);
                        isSuccess = newUser != null;
                        userObj = newUser ?? userObj;
                    }
                }
                else
                    return null;
            }

            return isSuccess ? userObj : null;
        }

        private bool ValidateUserInput(User user)
        {
            bool emailValid = user.UserName.IsValidEmail();
            bool namesValid = !string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName);
            return namesValid && emailValid; 
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
