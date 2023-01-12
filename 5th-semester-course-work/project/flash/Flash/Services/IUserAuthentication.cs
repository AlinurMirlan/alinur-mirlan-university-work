using Flash.Models.Identity;

namespace Flash.Services
{
    public interface IUserAuthentication
    {
        /// <summary>
        /// Sings the given user in, with the option to persist the authentication cookie.
        /// </summary>
        /// <param name="httpContext">HttpContext for authentication purposes.</param>
        /// <param name="user">User to sign in.</param>
        /// <param name="isPersistent">Option for persistent cookie storage.</param>
        /// <returns></returns>
        public Task SignUserInAsync(HttpContext httpContext, User user, bool isPersistent);

        /// <summary>
        /// Signs the given user out.
        /// </summary>
        /// <param name="httpContext">HttpContext for authentication purposes.</param>
        /// <returns></returns>
        public Task SignUserOutAsync(HttpContext httpContext);
    }
}
