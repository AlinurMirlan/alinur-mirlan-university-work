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
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string connectionString;

        public IngredientRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddIngredient(Ingredient ingredient)
        {
            SqlParameter[] inputParameters =
            {
                new SqlParameter("@name", ingredient.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                new SqlParameter("@price", ingredient.Price) { SqlDbType = SqlDbType.SmallMoney },
                new SqlParameter("@unitId", ingredient.UnitId) { SqlDbType = SqlDbType.Int }
            };
            SqlParameter outputParameter = new("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };

            using SqlConnection connection = new(connectionString);
            using SqlCommand dishCommand = new("InsertDish", connection) { CommandType = CommandType.StoredProcedure };
            dishCommand.Parameters.AddRange(inputParameters);
            dishCommand.Parameters.Add(outputParameter);

            connection.Open();
            dishCommand.ExecuteNonQuery();
            int ingredientId = (int)outputParameter.Value;
            return ingredientId;
        }

        public void DeleteIngredient(int ingredientId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("DeleteIngredient", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new SqlParameter("@ingredientId", ingredientId) { SqlDbType = SqlDbType.Int });
            connection.Open();
            command.ExecuteNonQuery();
        }

        public IList<Ingredient> GetDishIngredients(int dishId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetDishIngredients", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Ingredient> ingredients = new();
            while (reader.Read())
            {
                Ingredient ingredient = new
                (
                    name: reader.GetFieldValue<string>("Name"),
                    price: reader.GetFieldValue<decimal>("Price"),
                    unitId: reader.GetFieldValue<int>("UnitId")
                );
                ingredients.Add(ingredient);
            }

            return ingredients;
        }
    }
}
