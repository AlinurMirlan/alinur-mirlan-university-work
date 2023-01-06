using Flash.Infrastructure;
using Flash.Models;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Flash.Pages
{
    [Authorize]
    public class DecksModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IDeckRepository _deckRepo;
        private readonly ISessionManagement _session;

        public string DeckIdKey { get; set; }

        public int UserId { get; set; }

        public int SelectedDeckId { get; set; }

        public IEnumerable<Deck> Decks { get; set; } = default!;

        [BindProperty]
        public Deck PostedDeck { get; set; } = default!;

        public DecksModel(IConfiguration config, IDeckRepository deckRepo, ISessionManagement session)
        {
            _config = config;
            _deckRepo = deckRepo;
            _session = session;
            DeckIdKey = _config["SessionKeys:DeckId"] ?? throw new InvalidOperationException();
        }

        public async Task OnGetAsync(string? searchTerm = null)
        {
            int userId = HttpContext.User.GetClaimIntValue(_config["UserClaims:UserId"]);
            UserId = userId;
            if (searchTerm is null)
            {
                Decks = await _deckRepo.GetUserDecksAsync(userId);
            }
            else
            {
                Decks = await _deckRepo.GetUserDecksAsync(userId, searchTerm);
            }

            int? deckId = _session.SetDefaultDeck(HttpContext.Session, Decks);
            SelectedDeckId = deckId ?? 0;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deckRepo.AddAsync(PostedDeck);
            return RedirectToPage("/Decks", null);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int deckId)
        {
            await _deckRepo.DeleteAsync(deckId);
            if (HttpContext.Session.GetInt32(DeckIdKey) == deckId)
            {
                HttpContext.Session.Remove(DeckIdKey);
            }
            return RedirectToPage("/Decks", null);
        }

        public IActionResult OnPostSelect(int deckId)
        {
            HttpContext.Session.SetInt32(DeckIdKey, deckId);
            return RedirectToPage("/Decks", null);
        }
    }
}
