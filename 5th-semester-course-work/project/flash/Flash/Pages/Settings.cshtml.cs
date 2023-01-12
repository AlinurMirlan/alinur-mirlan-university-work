using Flash.Infrastructure;
using Flash.Models.Identity;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        [BindProperty]
        public UserEdit AccountEdit { get; set; } = default!;

        public User Account { get; set; } = default!;

        public static bool PasswordIsWrong { get; set; }

        public SettingsModel(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        public async Task OnGetAsync()
        {
            int userId = HttpContext.User.GetClaimIntValue(_config["UserClaims:UserId"]);
            Account = (await _userRepo.GetAsync(userId))!;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            PasswordIsWrong = !(await _userRepo.UpdateAsync(AccountEdit));
            return RedirectToPage();
        }
    }
}
