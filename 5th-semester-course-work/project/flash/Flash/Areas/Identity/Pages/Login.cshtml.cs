using Flash.Models.Identity;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Areas.Identity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserAuthentication _userAuth;

        [BindProperty]
        public User UserCredentials { get; set; } = default!;

        public LoginModel(IUserRepository userRepo, IUserAuthentication userAuth)
        {
            _userRepo = userRepo;
            _userAuth = userAuth;
        }

        public void OnGet()
        {
            ViewData["PageTitle"] = "Login page";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            User? user = await _userRepo.GetAsync(UserCredentials.EmailAddress, UserCredentials.Password);
            if (user is not null)
            {
                await _userAuth.SignUserInAsync(HttpContext, user);
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
