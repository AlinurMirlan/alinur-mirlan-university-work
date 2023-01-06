using Flash.Models.Identity;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Areas.Identity.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserAuthentication _userAuth;

        [BindProperty]
        public ConfirmedUser UserCredentials { get; set; } = default!;

        public RegisterModel(IUserRepository userRepo, IUserAuthentication userAuth)
        {
            _userRepo = userRepo;
            _userAuth = userAuth;
        }

        public void OnGet()
        {
            ViewData["PageTitle"] = "Registration page";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            User user;
            try
            {
                user = await _userRepo.AddAsync(UserCredentials);
            }
            catch (InvalidOperationException)
            {
                return Page();
            }

            await _userAuth.SignUserInAsync(HttpContext, user);
            return RedirectToPage("/Decks");
        }
    }
}
