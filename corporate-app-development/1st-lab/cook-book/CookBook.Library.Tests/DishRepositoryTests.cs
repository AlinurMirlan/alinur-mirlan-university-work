using Microsoft.Extensions.Configuration;
using CookBook.Library.Repositories;
using CookBook.Library.Entities;
using System.Data.SqlClient;

namespace CookBook.Library.Tests
{
    [TestFixture]
    public class DishRepositoryTests
    {
        private readonly IConfiguration config;

        public DishRepositoryTests()
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string jsonPath = Path.Combine(projectPath, "testsettings.json");
            this.config = new ConfigurationBuilder().AddJsonFile(jsonPath).Build();
        }

        [OneTimeSetUp]
        public void ClearDatabase()
        {
            using SqlConnection connection = new(config["ConnectionString"]);
            using SqlCommand command = new("DELETE FROM DishIngredients; DELETE FROM Dish;", connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        [TestCase("Beans", 2.2, 1)]
        [TestCase("Rice", 2.3, 1)]
        public void AddDish_WithoutIngredients_ReturnsNonNegativeInsertedDishsId(string name, decimal price, int typeId)
        {
            // Arrange
            DishRepository dishRepo = new(config["ConnectionString"]);

            // Act
            int id = dishRepo.AddDish(new Entities.Dish(name, price, typeId));

            // Assert
            Assert.That(id, Is.Not.Negative);
        }

        [Test]
        public void AddDish_WithIngredients_ReturnsNonNegativeInsertedDishsIdAndStoresTheIngredientsInDatabase()
        {
            // Arrange
            DishRepository dishRepo = new(config["ConnectionString"]);
            Entities.Dish dish = new("Macaroni", 1.9m, 1);
            // Actual values stored in the db
            (Entities.Ingredient, float)[] ingredients =
            {
                (new Ingredient("Картошка", 1.1m, 1) { Id = 1 }, .6f),
                (new Ingredient("Макароны", 2.5m, 1) { Id = 2 }, .2f),
            };
            var sqlCommand = (int id) => $"SELECT * FROM DishIngredients WHERE DishId = {id};";
            using SqlConnection connection = new(config["ConnectionString"]);
            int[] expectedIds = { 1, 2 };
            List<int> actualIds = new();

            // Act
            int id = dishRepo.AddDish(dish, ingredients);
            using SqlCommand command = new(sqlCommand(id), connection);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                actualIds.Add((int)reader["IngredientId"]);
            }

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(id, Is.Not.Negative);
                Assert.That(expectedIds, Is.EquivalentTo(actualIds));
            });
        }
    }
}