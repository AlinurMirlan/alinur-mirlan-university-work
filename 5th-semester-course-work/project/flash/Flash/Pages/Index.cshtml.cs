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
    public class IndexModel : PageModel
    {
        private readonly ISessionManagement _session;
        private readonly IFlashcardRepository _flashcardRepo;

        public Deck? Deck { get; set; }

        public IEnumerable<Flashcard> DueFlashcards { get; set; } = Enumerable.Empty<Flashcard>();

        public Pagination? Pagination { get; set; }

        public IndexModel(ISessionManagement session, IFlashcardRepository flashcardRepo)
        {
            _session = session;
            _flashcardRepo = flashcardRepo;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            Deck = await _session.SetDefaultDeckAsync(HttpContext);
            if (Deck is not null)
            {
                DueFlashcards = _flashcardRepo.GetDueFlashcardsPartitioned(Deck.Id, pageNumber, out int pageCount);
                Pagination = new(pageNumber, pageCount, "/Index");
                if (!DueFlashcards.Any() && pageCount != 0)
                {
                    return RedirectToPage(new { pageNumber = pageCount });
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int flashcardId, int newInterval)
        {   
            await _flashcardRepo.UpdateFlashcardAsync(flashcardId, newInterval);
            return RedirectToPage();
        }
    } 
}