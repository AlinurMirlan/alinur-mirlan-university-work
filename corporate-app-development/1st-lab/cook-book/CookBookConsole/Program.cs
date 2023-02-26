using CookBook.Library.Repositories;
using CookBook.Library.Repositories.Abstractions;
using System.Data.SqlClient;

IDishRepository dishRepo = new DishRepository("Server=(LocalDB)\\MSSQLLocalDB;Database=CookBook;Trusted_Connection=True;");
try
{
    dishRepo.AddDishIngredients(1010,
    new CookBook.Library.Entities.DishIngredient() { Name = "", Amount = 1 },
    new CookBook.Library.Entities.DishIngredient() { Name = "Овощь", Amount = 10 },
    new CookBook.Library.Entities.DishIngredient() { Name = "Подсолнечно масло", Amount = 10 },
    new CookBook.Library.Entities.DishIngredient() { Name = "Труба", Amount = 12});
}
catch (SqlException e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine("Errors:");
    foreach (SqlError error in e.Errors)
    {
        Console.WriteLine(error.Message);
    }
    Console.WriteLine("Inner exceptions:");
    if (e.InnerException is not null)
    {
        Console.WriteLine(e.InnerException.Message);
    }
}