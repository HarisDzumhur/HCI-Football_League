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
    public class StatisticsRepository : IStatisticsRepository
    {

        private readonly string _connectionString;

        public StatisticsRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public ObservableCollection<StatisticsModel> GetAllTeams(string year, string leagueName)
        {
            ObservableCollection<StatisticsModel> teams = new ObservableCollection<StatisticsModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT 
                s.brojBodova AS Points, 
                s.brojOdigranihUtakmica AS GamesPlayed, 
                s.brojPobjeda AS Wins, 
                s.brojPoraza AS Loses, 
                s.brojNerjesenih AS Draws, 
                s.brojPostignutihGolova AS GoalsScored, 
                s.brojPrimljenihGolova AS GoalsConceeded, 
                s.golRazlika AS GoalDifference, 
                t.logo AS Logo, 
                t.naziv AS Name 
                FROM STATISTIKA s JOIN TIM t ON s.TIM_naziv = t.naziv WHERE s.SEZONA_godina = @Year AND s.SEZONA_Liga_naziv = @LeagueName ORDER BY s.brojBodova DESC";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@LeagueName", leagueName);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int position = 1;

                    while (reader.Read())
                    {
                        teams.Add(new StatisticsModel
                        {
                            Position = position++,
                            Points = (int)reader["Points"],
                            GamesPlayed = (int)reader["GamesPlayed"],
                            Wins = (int)reader["Wins"],
                            Loses = (int)reader["Loses"],
                            Draws = (int)reader["Draws"],
                            GoalsScored = (int)reader["GoalsScored"],
                            GoalsConceeded = (int)reader["GoalsConceeded"],
                            GoalDifference = (int)reader["GoalDifference"],
                            Name = reader["Name"].ToString(),
                            Logo = (byte[])reader["Logo"]
                        });
                    }
                }
            }

            return teams;
        }

    }
}
