using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
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
    public class FavouritesRepository : IFavoritesRepository
    {
        private readonly string _connectionString;

        public FavouritesRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public void AddFollow(string teamName, int playerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "INSERT INTO KORISNIK_TIM (TIM_naziv, KORISNIK_idKorisnik) VALUES (@TeamName, @PlayerId)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamName", teamName);
                command.Parameters.AddWithValue("@PlayerId", playerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<FavouritesModel> GetFavouritesByUser(int playerId)
        {
            ObservableCollection<FavouritesModel> favourites = new ObservableCollection<FavouritesModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"WITH LatestMatches AS (
                        SELECT 
                            t.naziv AS TeamName,
                            t.logo AS TeamLogo,
                            u.datumUtakmice AS Date,
                            tsu.brojPostignutihGolova AS TeamGoalsScored,
                            tsu.brojPrimljenihGolova AS TeamGoalsConceeded,
                            at.naziv AS AwayTeamName,
                            at.logo AS AwayTeamLogo,
                            ROW_NUMBER() OVER (PARTITION BY t.naziv ORDER BY u.datumUtakmice DESC) AS RowNumber
                        FROM 
                            KORISNIK_TIM kt
                        JOIN 
                            TIM t ON kt.TIM_naziv = t.naziv
                        JOIN 
                            TIM_STATISTIKA_UTAKMICA tsu ON tsu.TIM_naziv = t.naziv
                        JOIN 
                            UTAKMICA u ON tsu.UTAKMICA_idUtakmica = u.idUtakmica
                        JOIN 
                            TIM at ON 
                                (at.naziv = (SELECT TIM_naziv FROM TIM_STATISTIKA_UTAKMICA WHERE UTAKMICA_idUtakmica = tsu.UTAKMICA_idUtakmica AND TIM_naziv != t.naziv) 
                                AND at.naziv != t.naziv)
                        WHERE 
                            kt.KORISNIK_idKorisnik = @PlayerId AND t.jeObrisan = 0
                    )
                    SELECT 
                        TeamName,
                        TeamLogo,
                        Date,
                        TeamGoalsScored,
                        TeamGoalsConceeded,
                        AwayTeamName,
                        AwayTeamLogo
                    FROM 
                        LatestMatches
                    WHERE 
                        RowNumber <= 3;";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerId", playerId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    var teamMap = new Dictionary<string, FavouritesModel>();

                    while (reader.Read())
                    {
                        string teamName = reader["TeamName"].ToString();
                        byte[] teamLogo = (byte[])reader["TeamLogo"];
                        DateTime date = Convert.ToDateTime(reader["Date"]);
                        int homeGoals = (int)reader["TeamGoalsScored"];
                        int awayGoals = (int)reader["TeamGoalsConceeded"];
                        string awayTeamName = reader["AwayTeamName"].ToString();
                        byte[] awayTeamLogo = (byte[])reader["AwayTeamLogo"];

                        if (!teamMap.ContainsKey(teamName))
                        {
                            var favourite = new FavouritesModel
                            {
                                HomeTeam = teamName,
                                HomeTeamLogo = teamLogo,
                                ResultModels = new ObservableCollection<ResultModel>()
                            };
                            favourites.Add(favourite);
                            teamMap[teamName] = favourite;
                        }

                        teamMap[teamName].ResultModels.Add(new ResultModel
                        {
                            Date = date,
                            HomeTeamGoals = homeGoals,
                            AwayTeamGoals = awayGoals,
                            AwayTeamName = awayTeamName,
                            AwayTeamLogo = awayTeamLogo
                        });
                    }
                }
            }

            return favourites;
        }


        public List<string> GetFollowsByUser(int playerId)
        {
            List<string> result = new List<string>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM KORISNIK_TIM WHERE KORISNIK_idKorisnik = @PlayerId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerId", playerId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader["TIM_naziv"].ToString());
                    }
                }
            }

            return result;
        }

        public void RemoveFollow(string teamName, int playerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "DELETE FROM KORISNIK_TIM WHERE TIM_naziv = @TeamName AND KORISNIK_idKorisnik = @PlayerId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamName", teamName);
                command.Parameters.AddWithValue("@PlayerId", playerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
