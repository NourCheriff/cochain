using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using CochainAPI.Model.Authentication;
using CochainAPI.Model.DTOs;
using CochainAPI.Model.DTOs.Configuration;
using CochainAPI.Model.Enums;
using CochainAPI.Model.Utils;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Authentication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using System.Data;

namespace CochainAPI.Authentication
{
    /// <summary>
    /// Class Auth Service.
    /// Implements the <see cref="SocialAuthentication.Interfaces.IAuthService" />
    /// </summary>
    /// <seealso cref="SocialAuthentication.Interfaces.IAuthService" />
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly Jwt _jwt;

        public AuthService(
            UserManager<User> userManager,
            IOptions<Jwt> jwt,
            IUserService userService)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _userService = userService;
        }

        /// <summary>
        /// Creates JWT Token
        /// </summary>
        /// <param name="user">the user</param>
        /// <returns>System.String</returns>
        private string CreateJwtToken(User user, List<IdentityRole> roles)
        {
            var key = Encoding.ASCII.GetBytes(_jwt.Secret);
            var userClaims = BuildUserClaims(user, roles);
            var signKey = new SymmetricSecurityKey(key);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.ValidIssuer,
                notBefore: DateTime.UtcNow,
                audience: _jwt.ValidAudience,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwt.DurationInMinutes)),
                claims: userClaims,
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// Builds the UserClaims
        /// </summary>
        /// <param name="user">the User</param>
        /// <returns>List&lt;System.Security.Claims&gt;</returns>
        private List<Claim> BuildUserClaims(User user, List<IdentityRole> roles)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name!)));

            return userClaims;
        }

        public async Task<BaseResponse<AuthenticateResponse>> SignInWithCredentials(AuthenticateRequest request)
        {
            var response = await _userService.Authenticate(request);
            if (response == null)
            {
                return new BaseResponse<AuthenticateResponse>("Invalid Credentials") { Status = RequestExecution.Error };
            }
            var userRoles = await _userService.GetRolesByUserId(response.Id);
            var token = CreateJwtToken(response, userRoles);
            var refreshToken = "";
            var data = new AuthenticateResponse(response, token, refreshToken);

            return new BaseResponse<AuthenticateResponse>(data);
        }

        public Task<BaseResponse<JwtResponseVM>> RefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GenerateTemporaryCredentials(AuthenticateRequest request)
        {
            var isGenerated = await _userService.GenerateTemporaryPassword(request);
            return isGenerated;
        }
    }
}