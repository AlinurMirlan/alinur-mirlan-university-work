using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Models;
using MusicPlatformApi.Repositories;
using System.Text.RegularExpressions;

namespace MusicPlatformApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IJwtTokenRepository _jwtRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenRepository jwtRepo, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _jwtRepo = jwtRepo;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet("present")]
        public async Task<ActionResult<bool>> Present(string email)
        {
            if (!Regex.Match(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").Success)
                return BadRequest("Invalid email");

            User? user = await _userManager.FindByEmailAsync(email);
            return Ok(user is not null);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove(string userId)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return BadRequest($"There is no user with the given id {userId}");

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return new StatusCodeResult(500);

            return Ok();
        }

        [HttpGet("users")]
        public ActionResult<PageModel<UserDto>> Users(int page = 1, int items = 6)
        {
            IEnumerable<User> users = _userManager.Users.Skip((page - 1) * items).Take(items);
            LinkedList<UserDto> userDtos = new();
            foreach (User user in users)
            {
                userDtos.AddLast(_mapper.Map<UserDto>(user));
            }

            return new PageModel<UserDto>
            {
                Page = page,
                PageSize = items,
                TotalPages = (int)Math.Ceiling((double)userDtos.Count / items),
                Results = userDtos
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]LoginCredentialModel credential)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User? user = await _userManager.FindByEmailAsync(credential.Email);
            if (user is null)
                return NotFound("User is not found");

            var result = await _signinManager.CheckPasswordSignInAsync(user, credential.Password, false);
            if (!result.Succeeded)
                return BadRequest("Password does not match");

            CredentialModel credentialModel = _jwtRepo.CreateJwt(user);
            string adminRoleString = _config["Security:Roles:Admin"] ?? throw new InvalidOperationException("Admin Role is not set up.");
            bool isAdmin = await _userManager.IsInRoleAsync(user, adminRoleString);
            credentialModel.IsAdmin = isAdmin;
            return Created(string.Empty, credentialModel);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody]RegistrationCredentialModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = _mapper.Map<User>(userModel);
            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
                return new BadRequestObjectResult("Failed to create user. Please check your inputs and try again.");
            
            CredentialModel credentialModel = _jwtRepo.CreateJwt(user);
            return Created(string.Empty, credentialModel);
        }
    }
}
