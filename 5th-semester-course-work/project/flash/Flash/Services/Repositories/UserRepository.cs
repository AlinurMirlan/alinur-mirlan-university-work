using Flash.Data;
using Flash.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flash.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashcardsContext _repository;

        public UserRepository(FlashcardsContext context)
        {
            _repository = context;
        }

        public async Task<User?> GetIncludedAsync(int userId) => await _repository.Users.Include(u => u.Decks.OrderBy(d => d.Name)).FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<User?> GetAsync(string email, string password) => await _repository.Users.FirstOrDefaultAsync(u => u.EmailAddress == email && u.Password == password);

        public async Task<User?> GetAsync(int userId) => await _repository.Users.FindAsync(userId);

        public async Task<bool> UpdateAsync(UserEdit updatedUser)
        {
            User? user = await _repository.Users.FirstOrDefaultAsync(u => u.EmailAddress == updatedUser.EmailAddress && u.Password == updatedUser.Password);
            if (user is null)
            {
                return false;
            }

            user.Password = updatedUser.NewPassword;
            await _repository.SaveChangesAsync();
            return true;
        }

        public Task<User> AddAsync(User user)
        {
            if (_repository.Users.Any(u => u.EmailAddress == user.EmailAddress))
                throw new InvalidOperationException("User with the same email address already exists in the database.");

            return AddAsyncInternal(user);
        }

        private async Task<User> AddAsyncInternal(User user)
        {
            user = (await _repository.Users.AddAsync(user)).Entity;
            await _repository.SaveChangesAsync();
            return user;
        }
    }
}
