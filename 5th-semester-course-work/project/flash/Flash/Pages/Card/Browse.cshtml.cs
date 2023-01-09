using Flash.Infrastructure;
using Flash.Models;
using Flash.Models.ViewModels;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Pages.Card
{
    [Authorize]
    public class BrowseModel : PageModel
    {
        private readonly ISessionManagement _session;
        private readonly IFlashcardRepository _flashcardRepo;

        public Deck? Deck { get; set; }

        public string? SearchTerm { get; set; }

        public IEnumerable<Flashcard> Flashcards { get; set; } = Enumerable.Empty<Flashcard>();

        public Pagination? Pagination { get; set; }

        public BrowseModel(IConfiguration config, ISessionManagement session, IDeckRepository deckRepo, IFlashcardRepository flashcardRepo)
        {
            _session = session;
            _flashcardRepo = flashcardRepo;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, string? searchTerm = null)
        {
            Deck = await _session.SetDefaultDeckAsync(HttpContext);
            if (Deck is not null)
            {
                Flashcards = _flashcardRepo.GetFlashcardsPartitioned(Deck.Id, pageNumber, searchTerm, out int pageCount);
                if (!Flashcards.Any() && pageCount != 0)
                {
                    return RedirectToPage(new { pageNumber = pageCount, searchTerm });
                }
                SearchTerm = searchTerm;
                Pagination = new(pageNumber, pageCount, "/Card/Browse");
            }

            return Page();
        }
    }
}
