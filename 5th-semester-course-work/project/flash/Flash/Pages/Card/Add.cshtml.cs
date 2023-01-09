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
        private readonly IConfiguration _config;
        private readonly ISessionManagement _session;
        private readonly IDeckRepository _deckRepo;
        private readonly IFlashcardRepository _flashcardRepo;

        public Deck? Deck { get; set; }

        [BindProperty]
        public Flashcard PostedFlashcard { get; set; } = default!;

        public AddModel(IConfiguration config, ISessionManagement session, IDeckRepository deckRepo, IFlashcardRepository flashcardRepo)
        {
            _config = config;
            _session = session;
            _deckRepo = deckRepo;
            _flashcardRepo = flashcardRepo;
        }

        public async Task OnGetAsync()
        {
            Deck = await _session.SetDefaultDeckAsync(HttpContext);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _flashcardRepo.AddAsync(PostedFlashcard);
            return RedirectToPage("/Card/Add", null);
        }
    }
}
