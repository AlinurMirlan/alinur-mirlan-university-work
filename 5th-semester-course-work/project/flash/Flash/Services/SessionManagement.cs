using Flash.Infrastructure;
using Flash.Models;
using Flash.Services.Repositories;

namespace Flash.Services
{
    public class SessionManagement : ISessionManagement
    {
        private readonly IDeckRepository _deckRepo;
        private readonly IConfiguration _config;

        public SessionManagement(IConfiguration config, IDeckRepository deckRepo)
        {
            _deckRepo = deckRepo;
            _config = config;
        }

        public Deck? SetDefaultDeck(ISession session, IEnumerable<Deck> decks)
        {
            Deck? deck;
            string deckKey = _config["SessionKeys:Deck"] ?? throw new InvalidOperationException();
            if (!session.Keys.Contains(deckKey))
            {
                deck = decks.FirstOrDefault();
                if (deck is not null)
                {
                    session.Set(deckKey, deck);
                }
            }
            else
            {
                deck = session.Get<Deck>(deckKey);
            }

            return deck;
        }

        public async Task<Deck?> SetDefaultDeckAsync(HttpContext httpContext)
        {
            Deck? deck;
            string deckKey = _config["SessionKeys:Deck"] ?? throw new InvalidOperationException();
            int userId = httpContext.User.GetClaimIntValue(_config["UserClaims:UserId"]);
            if (!httpContext.Session.Keys.Contains(deckKey))
            {
                deck = (await _deckRepo.GetDecksAsync(userId)).FirstOrDefault();
                if (deck is not null)
                {
                    httpContext.Session.Set(deckKey, deck);
                }
            }
            else
            {
                deck = httpContext.Session.Get<Deck>(deckKey);
            }

            return deck;
        }
    }
}
