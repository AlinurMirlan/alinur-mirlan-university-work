using Flash.Data;
using Flash.Services;
using Flash.Services.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

// Add services to the container.
builder
    .Services
    .AddRazorPages()
    .AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Index", "/Rehearse"))
    .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.Cookie.Name = "CookieAuthentication";
        options.LoginPath = "/Identity/Login";
    })
    .Services
    .AddDbContext<FlashcardsContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")))
    .AddSession()
    .AddDistributedMemoryCache()
    .AddSingleton<ITimeInterval, TimeInterval>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IDeckRepository, DeckRepository>()
    .AddScoped<IFlashcardRepository, FlashcardRepository>()
    .AddScoped<ISessionManagement, SessionManagement>()
    .AddScoped<IUserAuthentication, UserAuthentication>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
