using Flash.Models;

namespace Flash.Services
{
    public class SessionManagement : ISessionManagement
    {
        private readonly IConfiguration _config;

        public SessionManagement(IConfiguration config)
        {
            _config = config;
        }

        public int? SetDefaultDeck(ISession session, IEnumerable<Deck>? decks)
        {
            int? deckId;
            string deckIdKey = _config["SessionKeys:DeckId"] ?? throw new InvalidOperationException();
            if (!session.Keys.Contains(deckIdKey))
            {
                deckId = decks?.FirstOrDefault()?.Id;
                if (deckId is not null)
                {
                    session.SetInt32(deckIdKey, deckId.Value);
                }
            }
            else
            {
                deckId = session.GetInt32(deckIdKey);
            }

            return deckId;
        }
    }
}
