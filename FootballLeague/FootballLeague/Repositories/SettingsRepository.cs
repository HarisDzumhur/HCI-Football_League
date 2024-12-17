using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly string _connectionString;

        public SettingsRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public SettingsModel GetUserSettings(int settingsId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM POSTAVKE WHERE idPostavke = @SettingsId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@SettingsId", settingsId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new SettingsModel
                        {
                            Id = (int)reader["idPostavke"],
                            Theme = reader["tema"].ToString(),
                            Font = reader["font"].ToString(),
                            Language = reader["jezik"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public int GetSettingsId(string theme, string font, string language)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT IF(COUNT(*) = 1, (SELECT idPostavke FROM POSTAVKE WHERE jezik = @Language AND font = @Font AND tema = @Theme LIMIT 1), -1) AS Id 
                            FROM POSTAVKE
                            WHERE jezik = @Language AND font = @Font AND tema = @Theme";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Language", language);
                command.Parameters.AddWithValue("@Font", font);
                command.Parameters.AddWithValue("@Theme", theme);

                connection.Open();
                var result = Convert.ToInt32(command.ExecuteScalar());

                if (result != -1)
                    return result;
                else
                {
                    query = @"INSERT INTO POSTAVKE (tema, font, jezik) VALUES (@Theme, @Font, @Language);
                            SELECT LAST_INSERT_ID();";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Theme", theme);
                    command.Parameters.AddWithValue("@Font", font);
                    command.Parameters.AddWithValue("@Language", language);

                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    return newId;
                }
            }
        }
        public ObservableCollection<string> GetEnumValues(string column)
        {
            var enumValues = new ObservableCollection<string>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POSTAVKE' AND COLUMN_NAME = @ColumnName";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ColumnName", column);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var columnType = reader["COLUMN_TYPE"].ToString();

                        if (!string.IsNullOrEmpty(columnType) && columnType.StartsWith("enum(") && columnType.EndsWith(")"))
                        {
                            var enumValuesString = columnType.Substring(5, columnType.Length - 6);
                            var values = enumValuesString.Split(',')
                                .Select(v => v.Trim().Trim('\''))
                                .ToList();

                            foreach (var value in values)
                            {
                                enumValues.Add(value);
                            }
                        }
                    }
                }
            }

            return enumValues;
        }
    }
}
