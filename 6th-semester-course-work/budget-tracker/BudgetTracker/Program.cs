using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Infrastructure;
using BudgetTracker.Models;
using BudgetTracker.Repositories;
using BudgetTracker.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
string connectionString = config.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string is not initialized.");
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    // Configure password rules
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var logger = provider.GetRequiredService<ILogger<AutoMapperProfile>>();
    cfg.AddProfile(new AutoMapperProfile(contextAccessor, logger));
}).CreateMapper());
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));
// Add the processing server as IHostedService
builder.Services.AddHangfireServer();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "CookieAuthentication";
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Auth";
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<EntryRepository>();
builder.Services.AddTransient(provider =>
{
    var logger = provider.GetRequiredService<ILogger<RecurringJobs>>();
    var entryRepo = provider.GetRequiredService<EntryRepository>();
    var mapper = provider.GetRequiredService<IMapper>();
    return new RecurringJobs(entryRepo, mapper, logger);
});
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseHangfireDashboard();

app.MapControllerRoute(
    name: "Income",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
