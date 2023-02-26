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
    public class UnitRepository : IUnitRepository
    {
        private readonly string connectionString;

        public UnitRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<string> GetAllUnits()
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetAllUnits", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<string> units = new();
            while (reader.Read())
                units.Add(reader.GetFieldValue<string>("Name"));

            return units;
        }

        public string? GetUnit(int id)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetUnit", connection) { CommandType = CommandType.StoredProcedure};
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return reader.GetFieldValue<string>("Name");

            return null;
        }
    }
}
