using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CochainAPI.Model.Authentication;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.DTOs.Configuration;
using System.Data;
using System.Security.Claims;

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
                    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
                var role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;
                var user = await userService.GetById(userId);

                if (user != null)
                {
                    // Create ClaimsIdentity based on the user's claims
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.NameId, userId),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, role)  // Or loop through if you have multiple roles
                    };

                    var identity = new ClaimsIdentity(claims, "jwt");

                    // Create ClaimsPrincipal
                    var principal = new ClaimsPrincipal(identity);

                    // Attach ClaimsPrincipal to context
                    context.User = principal;
                    context.Items["User"] = principal;  // You can still keep your user object if needed
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
