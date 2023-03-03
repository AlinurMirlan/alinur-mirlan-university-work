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

        public int AddDish(Dish dish, params DishIngredient[] ingredients)
        {
            List<Exception> exceptions = new(2);
            ValidateDish(exceptions, dish);
            ValidateIngredients(exceptions, ingredients);
            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);

            SqlParameter[] inputParameters =
            {
                new SqlParameter("@name", dish.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                new SqlParameter("@price", dish.Price) { SqlDbType = SqlDbType.SmallMoney },
                new SqlParameter("@dishType", dish.DishType) { SqlDbType = SqlDbType.NVarChar, Size = 40 }
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
            }

            AddDishIngredients(dishId, ingredients);
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

        private void ValidateIngredients(List<Exception> exceptions, params DishIngredient[] ingredients)
        {
            using SqlConnection connection = new(connectionString);
            StringBuilder commandStringBuilder = new();
            using SqlCommand ingrCheckCommand = new(string.Empty, connection);
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(ingredients[i].Name))
                    continue;
                if (ingredients[i].Amount <= 0)
                    exceptions.Add(new ArgumentException($"Amount of ingredients has to be a positive number - {ingredients[i].Name}."));

                commandStringBuilder.Append($"EXEC DoesIngredientExist @ingredientName{i};");
                SqlParameter parameter = new($"@ingredientName{i}", ingredients[i].Name)
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 40
                };
                ingrCheckCommand.Parameters.Add(parameter);
            }

            if (commandStringBuilder.Length == 0)
            {
                exceptions.Add(new ArgumentException("Dish must at least contain one ingredient."));
                return;
            }

            connection.Open();
            ingrCheckCommand.CommandText = commandStringBuilder.ToString();
            try
            {
                ingrCheckCommand.ExecuteNonQuery();
            }
            catch (SqlException excpt)
            {
                exceptions.Add(excpt);
            }
        }

        private void ValidateDish(List<Exception> exceptions, Dish dish, Dish? oldDish = null)
        {
            if (string.IsNullOrEmpty(dish.DishType))
                exceptions.Add(new ArgumentException("Type of the dish cannnot be empty."));

            if (string.IsNullOrWhiteSpace(dish.Name))
            {
                exceptions.Add(new ArgumentException("Name of the dish cannnot be empty."));
                return;
            }

            if (oldDish is not null && dish.Name == oldDish.Name)
                return;

            using SqlConnection connection = new(connectionString);
            using SqlCommand dishCheckCommand = new("DoesDishExist", connection) { CommandType = CommandType.StoredProcedure };
            SqlParameter parameter = new($"@dishName", dish.Name)
            {
                SqlDbType = SqlDbType.NVarChar,
                Size = 40
            };
            dishCheckCommand.Parameters.Add(parameter);
            connection.Open();
            try
            {
                dishCheckCommand.ExecuteNonQuery();
            }
            catch (SqlException excpt) 
            { 
                exceptions.Add(excpt); 
            }
        }

        public void AddDishIngredients(int dishId, params DishIngredient[] ingredients)
        {
            if (!ingredients.Any())
                return;

            StringBuilder commandStringBuilder = new();
            using SqlConnection connection = new(connectionString);
            using SqlCommand ingrAddCommand = new(commandStringBuilder.ToString(), connection);
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (string.IsNullOrEmpty(ingredients[i].Name))
                    continue;

                commandStringBuilder.AppendLine($"EXEC InsertDishIngredient @dishId{i}, @ingredientName{i}, @amount{i};");
                SqlParameter[] parameters =
                {
                    new SqlParameter($"@dishId{i}", dishId) { SqlDbType = SqlDbType.Int },
                    new SqlParameter($"@ingredientName{i}", ingredients[i].Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                    new SqlParameter($"@amount{i}", ingredients[i].Amount) { SqlDbType = SqlDbType.Float },
                };
                ingrAddCommand.Parameters.AddRange(parameters);
            }

            connection.Open();
            ingrAddCommand.CommandText = commandStringBuilder.ToString();
            ingrAddCommand.ExecuteNonQuery();
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
                    dishType: reader.GetFieldValue<string>("DishType")
                )
                { Id = reader.GetFieldValue<int>("Id") };

                dish.Ingredients = (List<DishIngredient>)ingredientRepo.GetDishIngredients(dish.Id);
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
                    dishType: reader.GetFieldValue<string>("DishType")
                )
                { Id = reader.GetFieldValue<int>("Id") };
                IIngredientRepository ingredientRepo = new IngredientRepository(connectionString);
                dish.Ingredients = (List<DishIngredient>)ingredientRepo.GetDishIngredients(dish.Id);
                return dish;
            }

            return null;
        }

        public void EditDish(Dish oldDish, Dish newDish)
        {
            List<Exception> exceptions = new();
            StringBuilder commandBuilder = new();
            using SqlCommand sqlBatch = new();
            ValidateDish(exceptions, newDish, oldDish);
            if (exceptions.Count == 0 && (oldDish.Name != newDish.Name || oldDish.DishType != newDish.DishType))
            {
                commandBuilder.Append("EXEC EditDish @dishId, @newName, @newType;");
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter($"@dishId", oldDish.Id) { SqlDbType = SqlDbType.Int },
                new SqlParameter($"@newName", newDish.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                new SqlParameter($"@newType", newDish.DishType) { SqlDbType = SqlDbType.NVarChar, Size = 40 }
                };
                sqlBatch.Parameters.AddRange(parameters);
            }

            Dictionary<string, bool> nameRepetition = new();
            int oldLength = oldDish.Ingredients.Count;
            for (int i = 0; i < oldLength; i++)
            {
                DishIngredient oldIngredient = oldDish.Ingredients[i];
                DishIngredient newIngredient = newDish.Ingredients[i];
                if (!string.IsNullOrWhiteSpace(newIngredient.Name))
                {
                    if (nameRepetition.ContainsKey(newIngredient.Name))
                    {
                        if (!nameRepetition[newIngredient.Name])
                        {
                            exceptions.Add(new ArgumentException($"There are multiple ingredients with the name {newIngredient.Name}."));
                            nameRepetition[newIngredient.Name] = true;
                        }
                        continue;
                    }

                    nameRepetition[newIngredient.Name] = false;
                }


                if (string.IsNullOrWhiteSpace(newIngredient.Name) || oldIngredient.Name != newIngredient.Name)
                {
                    commandBuilder.Append($"EXEC DeleteDishIngredients @dishId{i}, @ingredientId{i};");
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter($"@dishId{i}", oldDish.Id) { SqlDbType = SqlDbType.Int },
                        new SqlParameter($"@ingredientId{i}", oldIngredient.Id) { SqlDbType = SqlDbType.Int }
                    };
                    sqlBatch.Parameters.AddRange(parameters);
                    if (!string.IsNullOrWhiteSpace(newIngredient.Name))
                    {
                        commandBuilder.AppendLine($"EXEC InsertDishIngredient @dishIdTwo{i}, @ingredientNameTwo{i}, @amountTwo{i};");
                        SqlParameter[] innerParameters = new SqlParameter[]
                        {
                            new SqlParameter($"@dishIdTwo{i}", oldDish.Id) { SqlDbType = SqlDbType.Int },
                            new SqlParameter($"@ingredientNameTwo{i}", newIngredient.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                            new SqlParameter($"@amountTwo{i}", newIngredient.Amount) { SqlDbType = SqlDbType.Float },
                        };
                        sqlBatch.Parameters.AddRange(innerParameters);
                    }
                }
                else if (oldIngredient.Amount != newIngredient.Amount)
                {
                    commandBuilder.Append($"EXEC EditDishIngredientAmount @dishId{i}, @ingredientId{i}, @newAmount{i};");
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter($"@dishId{i}", oldDish.Id) { SqlDbType = SqlDbType.Int },
                        new SqlParameter($"@ingredientId{i}", oldIngredient.Id) { SqlDbType = SqlDbType.Int },
                        new SqlParameter($"@newAmount{i}", newIngredient.Amount) { SqlDbType = SqlDbType.Int },
                    };
                    sqlBatch.Parameters.AddRange(parameters);
                }
            }

            int newLength = newDish.Ingredients.Count;
            if (oldLength != newLength)
            {
                for (int i = oldLength; i < newLength; i++)
                {
                    DishIngredient newIngredient = newDish.Ingredients[i];
                    if (string.IsNullOrWhiteSpace(newIngredient.Name))
                        continue;

                    if (nameRepetition.ContainsKey(newIngredient.Name))
                    {
                        if (!nameRepetition[newIngredient.Name])
                        {
                            exceptions.Add(new ArgumentException($"There are multiple ingredients with the name {newIngredient.Name}."));
                            nameRepetition[newIngredient.Name] = true;
                        }
                        continue;
                    }

                    nameRepetition[newIngredient.Name] = false;
                    commandBuilder.Append($"EXEC InsertDishIngredient @dishId{i}, @ingredientName{i}, @amount{i};");
                    SqlParameter[] parameters =
                    {
                        new SqlParameter($"@dishId{i}", oldDish.Id) { SqlDbType = SqlDbType.Int },
                        new SqlParameter($"@ingredientName{i}", newIngredient.Name) { SqlDbType = SqlDbType.NVarChar, Size = 40 },
                        new SqlParameter($"@amount{i}", newIngredient.Amount) { SqlDbType = SqlDbType.Float },
                    };
                    sqlBatch.Parameters.AddRange(parameters);
                }
            }

            ValidateIngredients(exceptions, newDish.Ingredients.ToArray());
            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);

            using SqlConnection connection = new(connectionString);
            sqlBatch.Connection= connection;
            if (commandBuilder.Length == 0)
                return;

            sqlBatch.CommandText = commandBuilder.ToString();
            connection.Open();
            sqlBatch.ExecuteNonQuery();
        }
    }
}
