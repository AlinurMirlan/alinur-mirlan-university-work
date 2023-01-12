using Flash.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Flash.Services
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly IConfiguration _config;

        public UserAuthentication(IConfiguration config)
        {
            _config = config;
        }

        public async Task SignUserInAsync(HttpContext httpContext, User user, bool isPersistent)
        {
            string userIdClaimType = _config["UserClaims:UserId"] ?? throw new NullReferenceException();
            List<Claim> userClaims = new()
            {
                new Claim(userIdClaimType, user.Id.ToString())
            };
            ClaimsIdentity claimsIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
        }
        
        public async Task SignUserOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Session.Clear();
        }
    }
}
