using Flash.Models;
using Flash.Models.ViewModels;

namespace Flash.Services.Repositories
{
    public interface IDeckRepository
    {
        public Task<Deck?> GetAsync(int deckId);
        public Task<IEnumerable<Deck>> GetDecksAsync(int userId);
        public IEnumerable<Deck> GetDecksPartitioned(int userId, int page, string? searchTerm, out int pageCount);
        public Task DeleteAsync(int deckId);
        public Task<Deck> AddAsync(Deck deck);
        public int GetDecksCountOnPage(int userId, int page, string? searchTerm);
    }
}
