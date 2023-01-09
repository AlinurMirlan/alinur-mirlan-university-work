using Flash.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Pages.Card
{
    public class DeleteModel : PageModel
    {
        private readonly IFlashcardRepository _flashcardRepo;

        public DeleteModel(IFlashcardRepository flashcardRepo)
        {
            _flashcardRepo = flashcardRepo;
        }

        public async Task<IActionResult> OnPostAsync(int flashcardId, string returnUrl)
        {
            await _flashcardRepo.DeleteAsync(flashcardId);
            return Redirect(returnUrl);
        }
    }
}
