using Flash.Data;
using Flash.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flash.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashcardsContext _databaseContext;

        public UserRepository(FlashcardsContext context)
        {
            _databaseContext = context;
        }

        public async Task<User?> GetAsync(int userId) => await _databaseContext.Users.Include(u => u.Decks.OrderBy(d => d.Name)).FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<User?> GetAsync(string email, string password) => await _databaseContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == email && u.Password == password);

        public Task<User> AddAsync(User user)
        {
            if (_databaseContext.Users.Any(u => u.EmailAddress == user.EmailAddress))
                throw new InvalidOperationException("User with the same email address already exists in the database.");

            return AddAsyncInternal(user);
        }

        private async Task<User> AddAsyncInternal(User user)
        {
            user = (await _databaseContext.Users.AddAsync(user)).Entity;
            await _databaseContext.SaveChangesAsync();
            return user;
        }
    }
}
