using AutoMapper;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        public AuthController(ILogger<AuthController> logger, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CredentialsVm credentials)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindByEmailAsync(credentials.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "Either email or password is wrong.");
                logger.LogInformation("Attempted login with a non-existent email: {email}", credentials.Email);
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(user, credentials.Password, credentials.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Either email or password is wrong.");
                logger.LogInformation("Attempted login with a wrong password for the email: {email}", credentials.Email);
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationVm credentials, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var user = mapper.Map<User>(credentials);
            var userWithTheSameEmail = await userManager.FindByEmailAsync(user.Email!);
            if (userWithTheSameEmail is not null)
            {
                ModelState.AddModelError(nameof(RegistrationVm.Email), "Email is taken.");
                return View();
            }

            var result = await userManager.CreateAsync(user, credentials.Password);
            if (!result.Succeeded)
            {
                logger.LogError("Something went wrong in the registration process.");
                ModelState.AddModelError("", "Something went wrong. Please, try again later.");
                return View();
            }

            await signInManager.SignInAsync(user, isPersistent: credentials.RememberMe);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
