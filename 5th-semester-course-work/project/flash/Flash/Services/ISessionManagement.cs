using Flash.Models;

namespace Flash.Services
{
    public interface ISessionManagement
    {
        public int? SetDefaultDeck(ISession session, IEnumerable<Deck>? decks);
    }
}
