using Flash.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flash.Areas.Identity.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IUserAuthentication _userAuthentication;

        public LogoutModel(IUserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _userAuthentication.SignUserOutAsync(HttpContext);
            return RedirectToPage("/Login", new { Area = "Identity" });
        }
    }
}
