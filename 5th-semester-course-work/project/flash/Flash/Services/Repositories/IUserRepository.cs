using Flash.Models.Identity;

namespace Flash.Services.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user with it's Decks navigation collection included.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>User with it's Decks navigation collection included</returns>
        public Task<User?> GetIncludedAsync(int userId);

        /// <summary>
        /// Gets a user based on their email address and password.
        /// </summary>
        /// <param name="email">Email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>User</returns>
        public Task<User?> GetAsync(string email, string password);

        /// <summary>
        /// Gets a user with the given Id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>User</returns>
        public Task<User?> GetAsync(int userId);

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">User to add.</param>
        /// <returns></returns>
        public Task<User> AddAsync(User user);

        /// <summary>
        /// Updates a user's password.
        /// </summary>
        /// <param name="updatedUser">UserEdit instance which contains a new password.</param>
        /// <returns>True if operation succeeds (old password matches), False if the old password is not matching.</returns>
        public Task<bool> UpdateAsync(UserEdit updatedUser);
    }
}
