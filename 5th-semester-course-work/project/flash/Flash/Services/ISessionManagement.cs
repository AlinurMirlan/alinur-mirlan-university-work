using Flash.Models;

namespace Flash.Services
{
    public interface ISessionManagement
    {
        public Deck? SetDefaultDeck(ISession session, IEnumerable<Deck> decks);
        public Task<Deck?> SetDefaultDeckAsync(HttpContext httpContext);
    }
}
