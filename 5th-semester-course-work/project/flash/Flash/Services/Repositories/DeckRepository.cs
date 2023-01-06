using Flash.Data;
using Flash.Models;
using Microsoft.EntityFrameworkCore;

namespace Flash.Services.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly FlashcardsContext _databaseContext;

        public DeckRepository(FlashcardsContext context)
        {
            _databaseContext = context;
        }

        public async Task<Deck?> GetAsync(int deckId) => await _databaseContext.Decks.FindAsync(deckId);

        public Task<Deck> AddAsync(Deck deck)
        {
            if (_databaseContext.Decks.Any(d => d.Name == deck.Name && d.UserId == deck.UserId))
                throw new InvalidOperationException("Deck with the same name already exists in the database.");

            return AddAsyncInternal(deck);
        }

        private async Task<Deck> AddAsyncInternal(Deck deck)
        {
            deck = (await _databaseContext.Decks.AddAsync(deck)).Entity;
            await _databaseContext.SaveChangesAsync();
            return deck;
        }

        public async Task DeleteAsync(int deckId)
        {
            Deck? deck = await _databaseContext.Decks.FindAsync(deckId);
            if (deck is not null)
            {
                _databaseContext.Remove(deck);
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Deck>> GetUserDecksAsync(int userId, string? deckNameSearchTerm = null)
        {
            IEnumerable<Deck> matchingDecks;
            if (deckNameSearchTerm is null)
            {
                matchingDecks = await (
                        from deck in _databaseContext.Decks
                        where deck.UserId == userId
                        orderby deck.CreationDate
                        select deck
                        ).ToArrayAsync();
            }
            else
            {
                matchingDecks = await (
                        from deck in _databaseContext.Decks
                        where deck.Name.Contains(deckNameSearchTerm) && deck.UserId == userId
                        orderby deck.CreationDate
                        select deck
                        ).ToArrayAsync();
            }

            return matchingDecks;
        }
    }
}
