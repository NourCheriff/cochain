using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CochainAPI.Model.Authentication;
using System.Security.Claims;

namespace CochainAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public string Policy { get; set; }

        public AuthorizeAttribute(string roles = null, string policy = null)
        {
            Roles = roles;
            Policy = policy;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (ClaimsPrincipal)context.HttpContext.User;
            
            if (user == null || !user.Identity!.IsAuthenticated)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            // Check roles if provided
            if (!string.IsNullOrEmpty(Roles))
            {
                var userRoles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                var requiredRoles = Roles.Split(',');

                if (!requiredRoles.Any(role => userRoles.Contains(role)))
                {
                    context.Result = new JsonResult(new { message = "Forbidden: Insufficient roles" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }

            // Check policy if provided
            if (!string.IsNullOrEmpty(Policy))
            {
                var policyClaim = user.FindFirst(Policy);
                if (policyClaim == null)
                {
                    context.Result = new JsonResult(new { message = "Forbidden: Insufficient policy" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }
        }
    }
}
