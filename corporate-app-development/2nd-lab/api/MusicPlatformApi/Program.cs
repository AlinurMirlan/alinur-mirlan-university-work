using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Data;
using MusicPlatformApi.Infrastructure;
using MusicPlatformApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MusicPlatformApi.Data.Triggers;
using MusicPlatformApi.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;
string securityKey = config["Security:Tokens:Key"] ?? throw new InvalidOperationException("JWT security key is not set up.");

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MusicDbInitializer>();
builder.Services.AddDbContext<MusicContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    options.UseTriggers(builder =>
    {
        builder.AddTrigger<SetSongSignature>();
    });
});
builder.Services.AddIdentity<User, IdentityRole>((options) =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<MusicContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer((options) =>
{
    // Registered claims
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        //  Audience answers to the question: "What domain does this token apply to?". It's intended to limit the scope of using the token.
        ValidAudience = config["Security:Tokens:Audience"],
        ValidateIssuer = true,
        // The authority who issued the token. Points to where the token should be consumed. It's there to tell apart different issuers.
        ValidIssuer = config["Security:Tokens:Issuer"],
        // A security key used to verify the signature of the token. (type of the token and the encryption algorithm are already there in the headers)
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddTransient<IJwtTokenRepository, JwtTokenRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
builder.Services.AddTransient<FileHandler>((_) =>
{
    string imagesBasePath = config["Folder:SongImages"] ?? throw new InvalidOperationException("Base folder for song images is not initialized.");
    string songsBasePath = config["Folder:Songs"] ?? throw new InvalidOperationException("Base folder for songs is not initialized.");
    return new FileHandler(imagesBasePath, songsBasePath);
});

var app = builder.Build();

// Singleton for seeding the database. CreateScope() method creates a room for a singleton to use the scoped services.
using (IServiceScope scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetService<ILogger<MusicDbInitializer>>();
    var dbInitializer = scope.ServiceProvider.GetService<MusicDbInitializer>();
    try
    {
        logger?.LogInformation("Preparing the database");
        dbInitializer?.SeedAsync().Wait();
    }
    catch (Exception exception)
    {
        logger?.LogError(exception, "Failed to seed the database");
    }
}


// Configure the HTTP request pipeline.
bool isDevelopment = app.Environment.IsDevelopment();
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
  
app.Run();
