using Flash.Infrastructure;
using Flash.Models;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Pages.Card
{
    public class AddModel : PageModel
    {
        private readonly ISessionManagement _session;
        private readonly IFlashcardRepository _flashcardRepo;

        public static bool IsInvalidInput { get; set; }

        public Deck? Deck { get; set; }

        [BindProperty]
        public Flashcard PostedFlashcard { get; set; } = default!;

        public AddModel(ISessionManagement session, IFlashcardRepository flashcardRepo)
        {
            _session = session;
            _flashcardRepo = flashcardRepo;
        }

        public async Task OnGetAsync()
        {
            Deck = await _session.SetDefaultDeckAsync(HttpContext);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(PostedFlashcard.Front) || string.IsNullOrEmpty(PostedFlashcard.Back))
            {
                IsInvalidInput = true;
                return RedirectToPage("/Card/Add");
            }

            await _flashcardRepo.AddAsync(PostedFlashcard);
            return RedirectToPage("/Card/Add");
        }
    }
}
