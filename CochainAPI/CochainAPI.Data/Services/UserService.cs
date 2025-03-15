using Microsoft.Extensions.Options;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Authentication;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using CochainAPI.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly ISupplyChainPartnerRepository _supplyChainPartnerRepository;
        private readonly ICertificationAuthorityRepository _certificationAuthorityRepository;
        private readonly UserManager<User> _userManager;
        private readonly string TRANSPORT_TYPE = "Trasportatore";
        private readonly string RAWMATERIAL_TYPE = "Materia Prima";
        private readonly string TRANSFORMATION_TYPE = "Trasformazione";
        private readonly string RESELLER_TYPE = "Rivenditore Dettaglio";
        private readonly string STOCKIST_TYPE = "Grossista";
        private readonly string STORAGE_TYPE = "Stoccaggio";

        public UserService(IOptions<AppSettings> appSettings, IEmailService emailService, IUserRepository userRepository, ISupplyChainPartnerRepository supplyChainPartnerRepository, ICertificationAuthorityRepository certificationAuthorityRepository, UserManager<User> userManager)
        {
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _userRepository = userRepository;
            _supplyChainPartnerRepository = supplyChainPartnerRepository;
            _certificationAuthorityRepository = certificationAuthorityRepository;
            _userManager = userManager;
        }

        public async Task<List<User>> GetAllActive()
        {
            return await _userRepository.GetAllActive();
        }

        public async Task<User?> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User?> AddUser(User userObj)
        {
            if (!ValidateUserInput(userObj))
                return null;

            var isSCP = !userObj.SupplyChainPartnerId.HasValue;
            var isCA = !userObj.CertificationAuthorityId.HasValue;
            if (!isSCP && !isCA)
                return null;

            List<string> roles = new List<string>();
            if (isSCP && Guid.TryParse(userObj.SupplyChainPartnerId.ToString(), out Guid scpId))
            {
                var scp = await _supplyChainPartnerRepository.GetSupplyChainPartnerById(scpId);
                if (scp != null)
                {
                    if (!string.IsNullOrEmpty(userObj.Role) && userObj.Role.Equals("Admin"))
                    {
                        roles.Add("AdminSCP");
                    }
                    else
                    {
                        roles.Add("UserSCP");
                    }
                    var newUser = await _userRepository.AddUser(userObj);
                    if (newUser != null)
                    {
                        await AssignScpRoles(newUser, scp, roles);
                    }

                    userObj = newUser ?? userObj;
                }
            }
            else if (isCA && Guid.TryParse(userObj.CertificationAuthorityId.ToString(), out Guid caId))
            {
                var ca = await _certificationAuthorityRepository.GetCertificationAuthorityById(caId);
                if (ca != null)
                {
                    if (!string.IsNullOrEmpty(userObj.Role) && userObj.Role.Equals("Admin"))
                    {
                        roles.Add("AdminCA");
                    }
                    else
                    {
                        roles.Add("UserCA");
                    }
                    var newUser = await _userRepository.AddUser(userObj);
                    if (newUser != null)
                    {
                        await AssignCaRoles(newUser, roles);
                    }
                    userObj = newUser ?? userObj;
                }
            }
            return null;
        }

        public async Task<User?> UpdateUser(User userObj)
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(userObj.Id))
            {
                var obj = await _userRepository.GetById(userObj.Id);
                if (obj != null)
                {
                    obj.FirstName = userObj.FirstName;
                    obj.LastName = userObj.LastName;
                    obj.IsActive = userObj.IsActive;
                    isSuccess = await _userRepository.UpdateUser(obj);
                }
            }

            return isSuccess ? userObj : null;
        }

        private async Task AssignCaRoles(User user, List<string> roles)
        {
            await _userManager.AddToRolesAsync(user, roles);
        }

        private async Task AssignScpRoles(User user, SupplyChainPartner scp, List<string> roles)
        {
            if (scp.SupplyChainPartnerType != null && !string.IsNullOrEmpty(scp.SupplyChainPartnerType.Name) && scp.SupplyChainPartnerType.Name.Equals(TRANSPORT_TYPE))
            {
                roles.Add("SCPTransporter");
            }
            if (scp.SupplyChainPartnerType != null && !string.IsNullOrEmpty(scp.SupplyChainPartnerType.Name) && scp.SupplyChainPartnerType.Name.Equals(RAWMATERIAL_TYPE))
            {
                roles.Add("SCPRawMaterial");
            }
            if (scp.SupplyChainPartnerType != null && !string.IsNullOrEmpty(scp.SupplyChainPartnerType.Name) && scp.SupplyChainPartnerType.Name.Equals(TRANSFORMATION_TYPE))
            {
                roles.Add("SCPTransformator");
            }

            await _userManager.AddToRolesAsync(user, roles);
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