using Flash.Data;
using Flash.Models;
using Flash.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Flash.Services.Repositories
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly FlashcardsContext _repository;
        private readonly IConfiguration _config;

        public FlashcardRepository(FlashcardsContext context, IConfiguration config)
        {
            _repository = context;
            _config = config;
        }

        public async Task<Flashcard> AddAsync(Flashcard flashcard)
        {
            flashcard = (await _repository.Flashcards.AddAsync(flashcard)).Entity;
            await _repository.SaveChangesAsync();
            return flashcard;
        }

        public async Task<Flashcard?> GetAsync(int flashcardId) => await _repository.Flashcards.FindAsync(flashcardId);

        public async Task UpdateFlashcardAsync(int flashcardId, string? front, string? back)
        {
            Flashcard? flashcard = await _repository.Flashcards.FindAsync(flashcardId);
            if (flashcard is null)
            {
                return;
            }

            if (flashcard.Front != front)
            {
                flashcard.Front = (front ?? string.Empty);
            }
            if (flashcard.Back != back)
            {
                flashcard.Back = (back ?? string.Empty);
            }

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateFlashcardAsync(int flashcardId, int newInterval)
        {
            Flashcard? flashcard = await _repository.Flashcards.FindAsync(flashcardId);
            if (flashcard is null)
                throw new InvalidOperationException();

            flashcard.RepetitionInterval = newInterval;
            flashcard.RepetitionDate = DateTime.Today.AddDays(newInterval);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int flashcardId)
        {
            Flashcard? flashcard = await _repository.Flashcards.FindAsync(flashcardId);
            if (flashcard is not null)
            {
                _repository.Remove(flashcard);
                await _repository.SaveChangesAsync();
            }
        }

        public IEnumerable<Flashcard> GetDueFlashcardsPartitioned(int deckId, int page, out int pageCount)
        {
            int itemsPerPage = int.Parse(_config["Pagination:ItemsPerPage:Rehearse"] ?? throw new InvalidOperationException());
            IEnumerable<Flashcard> matchingDecks = _repository.Flashcards.Where(f => f.DeckId == deckId && f.RepetitionDate!.Value <= DateTime.Today);
            pageCount = (int)Math.Ceiling((double)matchingDecks.Count() / itemsPerPage);
            matchingDecks = matchingDecks.Skip(itemsPerPage * (page - 1)).Take(itemsPerPage);
            return matchingDecks;
        }

        public IEnumerable<Flashcard> GetFlashcardsPartitioned(int deckId, int page, string? searchTerm, out int pageCount)
        {
            int itemsPerPage = int.Parse(_config["Pagination:ItemsPerPage:Rehearse"] ?? throw new InvalidOperationException());
            IEnumerable<Flashcard> matchingDecks = _repository.Flashcards
                .Where(f => f.DeckId == deckId &&
                    (string.IsNullOrEmpty(searchTerm) || (f.Front.Contains(searchTerm) || f.Back.Contains(searchTerm))));
            pageCount = (int)Math.Ceiling((double)matchingDecks.Count() / itemsPerPage);
            matchingDecks = matchingDecks.Skip(itemsPerPage * (page - 1)).Take(itemsPerPage);
            return matchingDecks;
        }

        public int GetCardsCountOnPage(int deckId, int page, string? searchTerm)
        {
            int itemsPerPage = int.Parse(_config["Pagination:ItemsPerPage:Decks"] ?? throw new InvalidOperationException());
            int cardsCount = _repository.Flashcards
                .Where(f => f.DeckId == deckId &&
                    (string.IsNullOrEmpty(searchTerm) || (f.Front.Contains(searchTerm) || f.Back.Contains(searchTerm))))
                .Skip(itemsPerPage * (page - 1))
                .Take(itemsPerPage).Count();
            return cardsCount;
        }
    }
}
