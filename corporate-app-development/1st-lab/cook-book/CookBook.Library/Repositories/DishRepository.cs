using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly string connectionString;

        public DishRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddDish(Dish dish, params (Ingredient ingredient, float amount)[] ingredients)
        {
            SqlParameter[] inputParameters =
            {
                new SqlParameter("@name", dish.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                new SqlParameter("@price", dish.Price) { SqlDbType = SqlDbType.SmallMoney },
                new SqlParameter("@dishTypeId", dish.DishTypeId) { SqlDbType = SqlDbType.Int }
            };
            SqlParameter outputParameter = new("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };

            int dishId = -1;
            using (SqlConnection connection = new(connectionString))
            {
                using SqlCommand dishCommand = new("InsertDish", connection) { CommandType = CommandType.StoredProcedure };
                dishCommand.Parameters.AddRange(inputParameters);
                dishCommand.Parameters.Add(outputParameter);

                connection.Open();
                dishCommand.ExecuteNonQuery();
                dishId = (int)outputParameter.Value;
                AddDishIngredients(connection, dishId, ingredients);
            }

            return dishId;
        }

        public void DeleteDish(int dishId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("DeleteDish", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new SqlParameter("@dishId", dishId) { SqlDbType = SqlDbType.Int });
            connection.Open();
            command.ExecuteNonQuery();
        }

        private static void AddDishIngredients(SqlConnection connection, int dishId, (Ingredient ingredient, float amount)[] ingredients)
        {
            if (!ingredients.Any())
                return;

            StringBuilder sqlCommand = new();
            for (int i = 0; i < ingredients.Length; i++)
                sqlCommand.AppendLine($"EXEC InsertDishIngredient @dishId{i}, @ingredientId{i}, @amount{i};");

            using SqlCommand dishIngCommand = new(sqlCommand.ToString(), connection);
            for (int i = 0; i < ingredients.Length; i++)
            {
                SqlParameter[] parameters =
                {
                        new SqlParameter($"@dishId{i}", dishId) { SqlDbType = SqlDbType.Int },
                        new SqlParameter($"@ingredientId{i}", ingredients[i].ingredient.Id) { SqlDbType = SqlDbType.Int },
                        new SqlParameter($"@amount{i}", ingredients[i].amount) { SqlDbType = SqlDbType.Float },
                };
                dishIngCommand.Parameters.AddRange(parameters);
            }

            dishIngCommand.ExecuteNonQuery();
        }

        public IList<Dish> GetDishes()
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetAllDishes", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Dish> dishes = new();
            IIngredientRepository ingredientRepo = new IngredientRepository(connectionString);
            while (reader.Read())
            {
                Dish dish = new
                (
                    name: reader.GetFieldValue<string>("Name"),
                    price: reader.GetFieldValue<decimal>("Price"),
                    dishTypeId: reader.GetFieldValue<int>("DishTypeId")
                )
                { Id = reader.GetFieldValue<int>("Id") };

                dish.Ingredients = (List<Ingredient>)ingredientRepo.GetDishIngredients(dish.Id);
                dishes.Add(dish);
            }

            return dishes;
        }

        public Dish? GetDish(int dishId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetDish", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new SqlParameter("@dishId", dishId) { SqlDbType = SqlDbType.Int });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Dish dish = new
                (
                    name: reader.GetFieldValue<string>("Name"),
                    price: reader.GetFieldValue<decimal>("Price"),
                    dishTypeId: reader.GetFieldValue<int>("DishTypeId")
                )
                { Id = reader.GetFieldValue<int>("Id") };
                IIngredientRepository ingredientRepo = new IngredientRepository(connectionString);
                dish.Ingredients = (List<Ingredient>)ingredientRepo.GetDishIngredients(dish.Id);
                return dish;
            }

            return null;
        }
    }
}
