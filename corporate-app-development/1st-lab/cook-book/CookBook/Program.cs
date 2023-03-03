using CookBook.Library.Repositories;
using CookBook.Library.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDishRepository, DishRepository>((f) => new DishRepository(config["ConnectionStrings:DefaultConnection"]));
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>((f) => new IngredientRepository(config["ConnectionStrings:DefaultConnection"]));
builder.Services.AddScoped<IUnitRepository, UnitRepository>((f) => new UnitRepository(config["ConnectionStrings:DefaultConnection"]));
builder.Services.AddScoped<ITabRepository, TabRepository>((f) => new TabRepository(config["ConnectionStrings:DefaultConnection"]));
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

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();
