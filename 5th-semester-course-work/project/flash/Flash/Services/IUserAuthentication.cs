using Flash.Models.Identity;

namespace Flash.Services
{
    public interface IUserAuthentication
    {
        public Task SignUserInAsync(HttpContext httpContext, User user);
        public Task SignUserOutAsync(HttpContext httpContext);
    }
}
