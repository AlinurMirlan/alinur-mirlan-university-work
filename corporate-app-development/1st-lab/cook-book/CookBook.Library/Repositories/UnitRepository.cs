using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly string connectionString;
        private static readonly TextInfo text = CultureInfo.CurrentCulture.TextInfo;

        public UnitRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddUnit(string unitName)
        {
            if (string.IsNullOrEmpty(unitName))
                throw new ArgumentException("Name of the unit cannot be empty.");

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("InsertUnit", connection) { CommandType = CommandType.StoredProcedure };
            SqlParameter outputParameter = new() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            SqlParameter inputParameter = new("@unitName", unitName) { SqlDbType = SqlDbType.NVarChar, Size = 20 };
            command.Parameters.AddRange(new[] { inputParameter, outputParameter });
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new ArgumentException(e.Message);
            }
            return (int)outputParameter.Value;
        }

        public void DeleteUnit(int id)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("DeleteUnit", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.Add(new SqlParameter("@unitId", id) { SqlDbType = SqlDbType.Int });
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EditUnitName(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name of the unit cannot be empty.");

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("EditUnitName", connection) { CommandType = CommandType.StoredProcedure };
            SqlParameter[] parameters =
            {
                new SqlParameter("@unitId", id),
                new SqlParameter("@newName", name)
            };
            command.Parameters.AddRange(parameters);
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public IEnumerable<Unit> GetAll()
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetAllUnitsAndIds", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Unit> units = new();
            while (reader.Read())
            {
                units.Add(new Unit() { Name = text.ToLower(reader.GetFieldValue<string>("Name")), Id = reader.GetFieldValue<int>("Id") });
            }

            return units;
        }

        public IEnumerable<string> GetAllUnits()
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetAllUnits", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<string> units = new();
            while (reader.Read())
                units.Add(text.ToLower(reader.GetFieldValue<string>("Name")));

            return units;
        }

        public string? GetUnit(int id)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new("GetUnit", connection) { CommandType = CommandType.StoredProcedure};
            command.Parameters.Add(new SqlParameter("@unitId", id) { SqlDbType = SqlDbType.Int });
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return text.ToLower(reader.GetFieldValue<string>("Name"));

            return null;
        }
    }
}
