using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CookBook.Library.Repositories
{
    public class TabRepository : ITabRepository
    {
        private readonly string connectionString;
        private static readonly Random random = new();
        private static int ArbitraryNumber => int.Parse(random.Next(10000000).ToString().PadLeft(6, '0'));

        public TabRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddTab(Tab tab)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand dishCommand = new("InsertTab", connection) { CommandType = CommandType.StoredProcedure };
            SqlParameter inputParameter = new("@tabNumber", ArbitraryNumber) { SqlDbType = SqlDbType.Int };
            SqlParameter outputParameter = new("@tabId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            dishCommand.Parameters.Add(inputParameter);
            dishCommand.Parameters.Add(outputParameter);

            connection.Open();
            dishCommand.ExecuteNonQuery();
            int tabId = (int)outputParameter.Value;
            AddTabDishes(tabId, tab.TabDishes);
            return tabId;
        }

        private void AddTabDishes(int tabId, List<TabDish> dishes)
        {
            if (!dishes.Any())
                return;

            StringBuilder commandStringBuilder = new();
            using SqlConnection connection = new(connectionString);
            using SqlCommand tabDishAddCommand = new(commandStringBuilder.ToString(), connection);
            for (int i = 0; i < dishes.Count; i++)
            {
                commandStringBuilder.AppendLine($"EXEC InsertTabDish @tabId{i}, @dishId{i}, @dishCount{i};");
                SqlParameter[] parameters =
                {
                    new SqlParameter($"@tabId{i}", tabId) { SqlDbType = SqlDbType.Int },
                    new SqlParameter($"@dishId{i}", dishes[i].Id) { SqlDbType = SqlDbType.Int },
                    new SqlParameter($"@dishCount{i}", dishes[i].Quantity) { SqlDbType = SqlDbType.Int },
                };
                tabDishAddCommand.Parameters.AddRange(parameters);
            }

            connection.Open();
            tabDishAddCommand.CommandText = commandStringBuilder.ToString();
            tabDishAddCommand.ExecuteNonQuery();
        }

        public Tab GetTabDishes(int tabId)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetTabDishes", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new("@tabId", tabId) { SqlDbType = SqlDbType.Int });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            IIngredientRepository ingredientRepo = new IngredientRepository(connectionString);
            Tab tab = new();
            while (reader.Read())
            {
                TabDish dish = new
                (
                    name: reader.GetFieldValue<string>("Name"),
                    price: reader.GetFieldValue<decimal>("Price"),
                    dishType: reader.GetFieldValue<string>("DishType"),
                    quantity: reader.GetFieldValue<int>("Amount")
                )
                { Id = reader.GetFieldValue<int>("Id") };
                dish.Ingredients = (List<DishIngredient>)ingredientRepo.GetDishIngredients(dish.Id);

                tab.TabDishes.Add(dish);
            }

            return tab;
        }

        public IList<Tab> GetTabs(bool orderByDescending)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetTabs", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new("@orderByDescending", orderByDescending ? 1 : 0) { SqlDbType = SqlDbType.Bit });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Tab> tabs = new();
            while (reader.Read())
            {
                Tab tab = new(reader.GetFieldValue<int>("TabNumber"), reader.GetFieldValue<DateTime>("OrderDate"))
                { Id = reader.GetFieldValue<int>("Id") };
                tabs.Add(tab);
            }

            return tabs;
        }

        public IList<Tab> GetTabsByDate(DateTime orderDate, bool orderByDescending)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetTabsByDate", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new("@date", orderDate) { SqlDbType = SqlDbType.Date });
            command.Parameters.Add(new("@orderByDescending", orderByDescending ? 1 : 0) { SqlDbType = SqlDbType.Bit });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Tab> tabs = new();
            while (reader.Read())
            {
                Tab tab = new(reader.GetFieldValue<int>("TabNumber"), reader.GetFieldValue<DateTime>("OrderDate"))
                { Id = reader.GetFieldValue<int>("Id") };
                tabs.Add(tab);
            }

            return tabs;
        }

        public IList<IngredientExpenditure> GetProvisionExpenditure(DateTime dateStart, DateTime dateEnd)
        {
            TextInfo text = CultureInfo.CurrentCulture.TextInfo;
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("IngredientsExpenditure", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new("@dateStart", dateStart) { SqlDbType = SqlDbType.Date });
            command.Parameters.Add(new("@dateEnd", dateEnd) { SqlDbType = SqlDbType.Date });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<IngredientExpenditure> expenditures = new();
            while (reader.Read())
            {
                IngredientExpenditure expenditure = new()
                {
                    Ingredient = text.ToTitleCase(reader.GetFieldValue<string>("IngredientName")),
                    Unit = reader.GetFieldValue<string>("Unit"),
                    Amount = reader.GetFieldValue<double>("Amount"),
                    Cost = reader.GetFieldValue<double>("TotalPrice")
                };

                expenditures.Add(expenditure);
            }

            return expenditures;
        }
    }
}
