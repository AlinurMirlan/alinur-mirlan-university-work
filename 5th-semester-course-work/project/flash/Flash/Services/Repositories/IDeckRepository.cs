using Flash.Models;

namespace Flash.Services.Repositories
{
    public interface IDeckRepository
    {
        /// <summary>
        /// Gets a deck with the given Id.
        /// </summary>
        /// <param name="deckId">Id of the deck.</param>
        /// <returns>Deck instance pertaining to the Id provided.</returns>
        public Task<Deck?> GetAsync(int deckId);

        /// <summary>
        /// Gets user's Decks.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>Collection of the user's decks</returns>
        public Task<IEnumerable<Deck>> GetDecksAsync(int userId);

        /// <summary>
        /// Gets a portion/part of the user's Decks (pagination).
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="page">Portion/part.</param>
        /// <param name="searchTerm">Search term predicate to apply for Deck's name when getting Decks.</param>
        /// <param name="pageCount">Amount of portions/parts overall.</param>
        /// <returns>Portion of the user's Decks</returns>
        public IEnumerable<Deck> GetDecksPartitioned(int userId, int page, string? searchTerm, out int pageCount);

        /// <summary>
        /// Deletes a deck.
        /// </summary>
        /// <param name="deckId">Id of the deck.</param>
        /// <returns></returns>
        public Task DeleteAsync(int deckId);

        /// <summary>
        /// Adds a deck.
        /// </summary>
        /// <param name="deck">Deck to add.</param>
        /// <returns></returns>
        public Task<Deck> AddAsync(Deck deck);
    }
}
