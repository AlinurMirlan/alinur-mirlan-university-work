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

        private void ValidateIngredient(List<Exception> exceptions, Ingredient ingredient, Ingredient? oldIngredient = null)
        {
            if (ingredient.Price <= 0)
                exceptions.Add(new ArgumentException("Price has to be a positive number."));
            if (string.IsNullOrWhiteSpace(ingredient.Name))
            {
                exceptions.Add(new ArgumentException("Name of the ingredient cannot be empty."));
                return;
            }
            if (oldIngredient is not null && ingredient.Name == oldIngredient.Name)
                return;

            using SqlConnection connection = new(connectionString);
            using SqlCommand validateCommand = new("DoesIngredientNotExist", connection) { CommandType = CommandType.StoredProcedure };
            SqlParameter parameter = new($"@ingredientName", ingredient.Name)
            {
                SqlDbType = SqlDbType.NVarChar,
                Size = 40
            };
            validateCommand.Parameters.Add(parameter);

            connection.Open();
            try
            {
                validateCommand.ExecuteNonQuery();
            }
            catch (SqlException excpt)
            {
                exceptions.Add(excpt);
            }
        }

        public void EditIngredient(Ingredient oldIngredient, Ingredient newIngredient)
        {
            if (string.Equals(oldIngredient.Name, newIngredient.Name, StringComparison.OrdinalIgnoreCase) && oldIngredient.Price == newIngredient.Price && oldIngredient.Unit == newIngredient.Unit)
                return;
            List<Exception> exceptions = new();
            ValidateIngredient(exceptions, newIngredient, oldIngredient);
            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);

            using SqlConnection connection = new(connectionString);
            using SqlCommand editCommand = new("EditIngredient", connection) { CommandType = CommandType.StoredProcedure};
            SqlParameter[] parameters =
            {
                new SqlParameter("@ingredientId", oldIngredient.Id) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@newName", newIngredient.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                new SqlParameter("@newPrice", newIngredient.Price) { SqlDbType = SqlDbType.SmallMoney },
                new SqlParameter("@newUnit", newIngredient.Unit) { SqlDbType = SqlDbType.NVarChar, Size = 20 }
            };

            connection.Open();
            editCommand.Parameters.AddRange(parameters);
            editCommand.ExecuteNonQuery();
        }

        public int AddIngredient(Ingredient ingredient)
        {
            List<Exception> exceptions = new();
            ValidateIngredient(exceptions, ingredient);
            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);

            SqlParameter[] inputParameters =
            {
                new SqlParameter("@name", ingredient.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                new SqlParameter("@price", ingredient.Price) { SqlDbType = SqlDbType.SmallMoney },
                new SqlParameter("@unitName", ingredient.Unit) { SqlDbType = SqlDbType.NVarChar, Size = 20 }
            };
            SqlParameter outputParameter = new("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };

            using SqlConnection connection = new(connectionString);
            using SqlCommand dishCommand = new("InsertIngredient", connection) { CommandType = CommandType.StoredProcedure };
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

        public IList<DishIngredient> GetDishIngredients(int dishId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetDishIngredients", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new SqlParameter("@dishId", dishId) { SqlDbType = SqlDbType.Int });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<DishIngredient> ingredients = new();
            while (reader.Read())
            {
                DishIngredient ingredient = new
                (
                    name: reader.GetFieldValue<string>("Name"),
                    price: reader.GetFieldValue<decimal>("Price"),
                    unit: reader.GetFieldValue<string>("Unit"),
                    amount: reader.GetFieldValue<double>("Amount")
                )
                { Id = reader.GetFieldValue<int>("Id") };
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public IList<Ingredient> GetIngredients()
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetIngredients", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Ingredient> ingredients = new();
            while (reader.Read())
            {
                Ingredient ingredient = new
                (
                    name: reader.GetFieldValue<string>("Name"),
                    price: reader.GetFieldValue<decimal>("Price"),
                    unit: reader.GetFieldValue<string>("Unit")
                )
                { Id = reader.GetFieldValue<int>("Id") };
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public Ingredient? GetIngredient(int ingredientId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetIngredient", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new SqlParameter("@id", ingredientId) { SqlDbType = SqlDbType.Int });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
                return null;

            Ingredient ingredient = new
            (
                name: reader.GetFieldValue<string>("Name"),
                price: reader.GetFieldValue<decimal>("Price"),
                unit: reader.GetFieldValue<string>("Unit")
            )
            { Id = reader.GetFieldValue<int>("Id") };
            return ingredient;
        }
    }
}
