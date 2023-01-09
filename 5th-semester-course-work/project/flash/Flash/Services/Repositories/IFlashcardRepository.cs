using Flash.Models;
using Flash.Models.ViewModels;

namespace Flash.Services.Repositories
{
    public interface IFlashcardRepository
    {
        public Task<Flashcard> AddAsync(Flashcard flashcard);
        public Task<Flashcard?> GetAsync(int flashcardId);
        public Task UpdateFlashcardAsync(int flashcardId, string? front, string? back);
        public Task UpdateFlashcardAsync(int flashcardId, int newInterval);
        public Task DeleteAsync(int flashcardId);
        public IEnumerable<Flashcard> GetDueFlashcardsPartitioned(int deckId, int page, out int pageCount);
        public IEnumerable<Flashcard> GetFlashcardsPartitioned(int deckId, int page, string? searchTerm, out int pageCount);
    }
}
