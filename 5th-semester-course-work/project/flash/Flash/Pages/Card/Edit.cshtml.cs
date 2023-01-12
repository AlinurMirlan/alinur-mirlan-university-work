using Flash.Infrastructure;
using Flash.Models;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Pages.Card
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IFlashcardRepository _flashcardRepo;

        public static string ReturnUrlPage { get; set; } = string.Empty;

        public static bool IsInvalidInput { get; set; }

        public Deck Deck { get; set; } = default!;

        [BindProperty]
        public Flashcard Flashcard { get; set; } = default!;

        public EditModel(IConfiguration config, IFlashcardRepository flashcardRepo)
        {
            _config = config;
            _flashcardRepo = flashcardRepo;
        }

        public async Task OnGetAsync(int flashcardId, string returnUrl)
        {
            string deckKey = _config["SessionKeys:Deck"] ?? throw new InvalidOperationException();
            Deck = HttpContext.Session.Get<Deck>(deckKey)!;
            Flashcard = (await _flashcardRepo.GetAsync(flashcardId))!;
            ReturnUrlPage = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Flashcard.Front) || string.IsNullOrEmpty(Flashcard.Back))
            {
                IsInvalidInput = true;
                return RedirectToPage(new { flashcardId = Flashcard.Id, returnUrl = ReturnUrlPage });
            }

            await _flashcardRepo.UpdateFlashcardAsync(Flashcard.Id, Flashcard.Front, Flashcard.Back);
            return Redirect(ReturnUrlPage);
        }
    }
}
