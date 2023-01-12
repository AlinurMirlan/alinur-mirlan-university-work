using Flash.Models;

namespace Flash.Services
{
    public interface ISessionManagement
    {
        /// <summary>
        /// Sets a default deck in the session storage culled from the given Decks collection, or fetches one if the session already stores a deck.
        /// </summary>
        /// <param name="session">Session instance to operate on.</param>
        /// <param name="decks">Collection of Decks from which to pick a default deck, in case we haven't stored one in the session.</param>
        /// <returns>Deck stored in the session, or a default one picked from the collection of Decks supplied.</returns>
        public Deck? SetDefaultDeck(ISession session, IEnumerable<Deck> decks);

        /// <summary>
        /// Sets a default deck in the session storage culled from the database, or fetches one if the session already stores a deck.
        /// </summary>
        /// <param name="httpContext">HttpContext to access the current user(to access their collection of decks) and the session instance.</param>
        /// <returns>Deck stored in the session, or a default one picked from the database.</returns>
        public Task<Deck?> SetDefaultDeckAsync(HttpContext httpContext);
    }
}
