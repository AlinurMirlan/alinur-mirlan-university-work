using Flash.Models;

namespace Flash.Services.Repositories
{
    public interface IDeckRepository
    {
        public Task<Deck?> GetAsync(int deckId);
        public Task<IEnumerable<Deck>> GetUserDecksAsync(int userId, string? deckNameSearchTerm = null);
        public Task DeleteAsync(int deckId);
        public Task<Deck> AddAsync(Deck deck);
    }
}
