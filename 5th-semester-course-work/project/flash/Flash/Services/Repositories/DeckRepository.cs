using Flash.Data;
using Flash.Models;
using Microsoft.EntityFrameworkCore;

namespace Flash.Services.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly FlashcardsContext _repository;
        private readonly IConfiguration _config;

        public DeckRepository(FlashcardsContext context, IConfiguration config)
        {
            _repository = context;
            _config = config;
        }

        public async Task<Deck?> GetAsync(int deckId) => await _repository.Decks.FindAsync(deckId);

        public Task<Deck> AddAsync(Deck deck)
        {
            if (_repository.Decks.Any(d => d.Name == deck.Name && d.UserId == deck.UserId))
                throw new InvalidOperationException("Deck with the same name already exists in the database.");

            return AddAsyncInternal(deck);
        }

        private async Task<Deck> AddAsyncInternal(Deck deck)
        {
            deck = (await _repository.Decks.AddAsync(deck)).Entity;
            await _repository.SaveChangesAsync();
            return deck;
        }

        public async Task DeleteAsync(int deckId)
        {
            Deck? deck = await _repository.Decks.FindAsync(deckId);
            if (deck is not null)
            {
                _repository.Remove(deck);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Deck>> GetDecksAsync(int userId)
        {
            IEnumerable<Deck> matchingDecks = await (
                    from deck in _repository.Decks
                    where deck.UserId == userId
                    orderby deck.CreationDate
                    select deck
                    ).ToArrayAsync();
            return matchingDecks;
        }

        public IEnumerable<Deck> GetDecksPartitioned(int userId, int page, string? searchTerm, out int pageCount)
        {
            int itemsPerPage = int.Parse(_config["Pagination:ItemsPerPage:Decks"] ?? throw new InvalidOperationException());
            IEnumerable<Deck> matchingDecks = _repository.Decks
                .Where(d => d.UserId == userId && (string.IsNullOrEmpty(searchTerm) || d.Name.Contains(searchTerm)));
            pageCount = (int)Math.Ceiling((double)matchingDecks.Count() / itemsPerPage);
            matchingDecks = matchingDecks.Skip(itemsPerPage * (page - 1)).Take(itemsPerPage);
            return matchingDecks;
        }
    }
}
