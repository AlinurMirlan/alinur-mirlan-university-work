using Flash.Models.Identity;

namespace Flash.Services.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetAsync(int userId);
        public Task<User?> GetAsync(string email, string password);
        public Task<User> AddAsync(User user);
    }
}
