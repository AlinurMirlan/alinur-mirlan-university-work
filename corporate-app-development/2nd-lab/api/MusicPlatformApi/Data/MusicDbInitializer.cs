using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Infrastructure;
using System.Globalization;

namespace MusicPlatformApi.Data
{
    public class MusicDbInitializer
    {
        private readonly MusicContext _context;
        private readonly ILogger<MusicDbInitializer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _config;

        public MusicDbInitializer(MusicContext context, ILogger<MusicDbInitializer> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _environment = environment;
            _config = config;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!_userManager.Users.Any())
            {   
                User user = new()
                {
                    Name = "Alinur",
                    Email = "alinur@gmail.com",
                    UserName = "alinur@gmail.com",
                    Age = 20,
                    Sex = "m"
                };

                IdentityResult result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException("Failed to create user while seeding database.");

                result = await _userManager.AddPasswordAsync(user, "P@ssw0rd?");
                if (!result.Succeeded)
                    throw new InvalidOperationException("Failed to set password while seeding database.");

                string adminRoleString = _config["Security:Roles:Admin"] ?? throw new InvalidOperationException("Admin Role is not set up.");
                IdentityRole role = new(adminRoleString);
                result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                    throw new InvalidOperationException("Failed to create the Admin role while seeding database.");

                result = await _userManager.AddToRoleAsync(user, adminRoleString);
                if (!result.Succeeded)
                    throw new InvalidOperationException("Failed to add the Admin role to the dummy account while seeding database.");
            }

            if (!_context.Songs.Any())
            {
                _logger.LogInformation("Calling seeding of the database");
                string csvFilePath = @$"{_environment.ContentRootPath}/Data/songs.csv";
                SongReader songReader = new(csvFilePath: csvFilePath);
                Song[] songs = songReader.ReadSongs();
                _context.AddRange(songs);
                await _context.SaveChangesAsync();
            }
        }
    }
}
