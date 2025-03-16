using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.DTOs.Configuration;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CochainAPI.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Jwt _jwt;

        public JwtMiddleware(RequestDelegate next, IOptions<Jwt> jwt)
        {
            _next = next;
            _jwt = jwt.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userService, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwt.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
                var jti = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                var roles = jwtToken.Claims.Where(x => x.Type == ClaimTypes.Role);
                var user = await userService.GetById(userId);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.UserName!),
                        new Claim(JwtRegisteredClaimNames.Jti, jti),
                    };
                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Value)));

                    var identity = new ClaimsIdentity(claims, "jwt");
                    var principal = new ClaimsPrincipal(identity);
                    context.User = principal;
                    context.Items["User"] = principal;
                }
            }
            catch
            {
                //Do nothing if JWT validation fails
                // user is not attached to context so the request won't have access to secure routes
            }
        }
    }
}
