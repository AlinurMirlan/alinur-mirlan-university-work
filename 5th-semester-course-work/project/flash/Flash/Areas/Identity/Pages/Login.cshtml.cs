using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Flash.Areas.Identity.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        /*        public async Task<IActionResult> OnPostAsync()
                {
                    if (!ModelState.IsValid) 
                        return Page();

                    if (Credential.Email == "alinur@gmail.com" && Credential.Password == "password")
                    {
                        List<Claim> claims = new()
                        {
                            new Claim(ClaimTypes.Email, "alinur@gmail.com"),
                            new Claim("User", "user")
                        };
                        ClaimsIdentity identity = new(claims, "MyCookieAuth");
                        ClaimsPrincipal claimsPrincipal = new(identity);

                        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                        return RedirectToPage("/Index");
                    }

                    return Page();
                }*/
    }
}
