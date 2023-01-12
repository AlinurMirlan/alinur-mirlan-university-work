using Flash.Infrastructure;
using Flash.Models;
using Flash.Models.ViewModels;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Pages
{
    [Authorize]
    public class DecksModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IDeckRepository _deckRepo;
        private readonly ISessionManagement _session;

        public static int UserId { get; set; }

        public static bool DeckExists { get; set; }

        public Pagination Pagination { get; set; } = default!;

        public string DeckIdKey { get; set; }

        public int SelectedDeckId { get; set; }

        public string? SearchTerm { get; set; }

        public IEnumerable<Deck> Decks { get; set; } = Enumerable.Empty<Deck>();

        [BindProperty]
        public Deck DeckPost { get; set; } = default!;

        public DecksModel(IConfiguration config, IDeckRepository deckRepo, ISessionManagement session)
        {
            _config = config;
            _deckRepo = deckRepo;
            _session = session;
            DeckIdKey = _config["SessionKeys:Deck"] ?? throw new InvalidOperationException();
        }

        public IActionResult OnGet(int pageNumber = 1, string? searchTerm = null)
        {
            UserId = HttpContext.User.GetClaimIntValue(_config["UserClaims:UserId"]);
            Decks = _deckRepo.GetDecksPartitioned(UserId, pageNumber, searchTerm, out int pageCount);
            if (!Decks.Any() && pageCount != 0)
            {   
                return RedirectToPage(new { pageNumber = pageCount, searchTerm });
            }

            SearchTerm = searchTerm;
            Pagination = new(pageNumber, pageCount, "/Decks");
            Deck? deck = _session.SetDefaultDeck(HttpContext.Session, Decks);
            SelectedDeckId = deck?.Id ?? 0;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (string.IsNullOrEmpty(DeckPost.Name))
            {
                return Redirect(returnUrl);
            }

            try
            {
                await _deckRepo.AddAsync(DeckPost);
            }
            catch (InvalidOperationException)
            {
                DeckExists = true;
            }

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int deckId, string returnUrl)
        {
            await _deckRepo.DeleteAsync(deckId);
            Deck? deck = HttpContext.Session.Get<Deck>(DeckIdKey);
            if (deck?.Id == deckId)
            {
                HttpContext.Session.Remove(DeckIdKey);
            }

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> OnPostSelect(int deckId, string returnUrl)
        {
            Deck? deck = await _deckRepo.GetAsync(deckId);
            HttpContext.Session.Set(DeckIdKey, deck);
            return Redirect(returnUrl);
        }
    }
}
