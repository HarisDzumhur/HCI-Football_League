using FootballLeague.Models;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using MaterialDesignThemes.Wpf;
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
    public class LeagueRepository : ILeagueRepository
    {
        private readonly string _connectionString;

        public LeagueRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public void AddLeague(string leagueName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO LIGA (naziv) VALUES (@LeagueName)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LeagueName", leagueName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddMatchday(int matchdayNumber, string year, string leagueName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO KOLO (brojKola, SEZONA_godina, SEZONA_LIGA_naziv) VALUES (@MatchdayNumber, @Year, @LeagueName)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LeagueName", leagueName);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@MatchdayNumber", matchdayNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddSeason(string year, string leagueName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO SEZONA (godina, LIGA_naziv) VALUES (@Year, @LeagueName)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LeagueName", leagueName);
                command.Parameters.AddWithValue("@Year", year);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<string> GetAllLeagues()
        {
            ObservableCollection<string> leagues = new ObservableCollection<string>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM LIGA";
                var command = new MySqlCommand(query, connection);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        leagues.Add(reader["naziv"].ToString());
                    }
                }
            }

            return leagues;
        }

        public ObservableCollection<int> GetAllMatchdaysBySeason(string leagueName, string year)
        {
            ObservableCollection<int> matchdays = new ObservableCollection<int>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT brojKola FROM KOLO WHERE SEZONA_LIGA_naziv = @LeagueName AND SEZONA_godina = @Year";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LeagueName", leagueName);
                command.Parameters.AddWithValue("@Year", year);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matchdays.Add((int)reader["brojKola"]);
                    }
                }
            }

            return matchdays;
        }

        public ObservableCollection<string> GetAllSeasonsByLeague(string leagueName)
        {
            ObservableCollection<string> seasons = new ObservableCollection<string>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT godina FROM SEZONA WHERE LIGA_naziv = @LeagueName";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LeagueName", leagueName);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seasons.Add(reader["godina"].ToString());
                    }
                }
            }

            return seasons;
        }
    }
}
