using FootballLeague.Models;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly string _connectionString;

        public GamesRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public GamesModel AddGame(GamesModel game)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var query = @"INSERT INTO UTAKMICA (datumUtakmice, sudija, KOLO_brojKola, KOLO_SEZONA_godina, KOLO_SEZONA_Liga_naziv) 
                              VALUES (@Date, @Referee, @Matchday, @Season, @League); 
                              SELECT LAST_INSERT_ID();";
                        using (var command = new MySqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Date", game.Date);
                            command.Parameters.AddWithValue("@Referee", game.Referee);
                            command.Parameters.AddWithValue("@Matchday", game.Matchday);
                            command.Parameters.AddWithValue("@Season", game.Season);
                            command.Parameters.AddWithValue("@League", game.League);

                            game.Id = Convert.ToInt32(command.ExecuteScalar());
                        }

                        using (var command = new MySqlCommand("InsertTIM_STATISTIKA_UTAKMICA", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("p_TIM_naziv", game.HomeTeam);
                            command.Parameters.AddWithValue("p_UTAKMICA_idUtakmica", game.Id);
                            command.Parameters.AddWithValue("p_brojPostignutihGolova", game.HomeTeamGoals);
                            command.Parameters.AddWithValue("p_brojPrimljenihGolova", game.AwayTeamGoals);
                            command.Parameters.AddWithValue("p_jeDomacin", true);
                            command.ExecuteNonQuery();
                        }

                        using (var command = new MySqlCommand("InsertTIM_STATISTIKA_UTAKMICA", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("p_TIM_naziv", game.AwayTeam);
                            command.Parameters.AddWithValue("p_UTAKMICA_idUtakmica", game.Id);
                            command.Parameters.AddWithValue("p_brojPostignutihGolova", game.AwayTeamGoals);
                            command.Parameters.AddWithValue("p_brojPrimljenihGolova", game.HomeTeamGoals);
                            command.Parameters.AddWithValue("p_jeDomacin", false);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                return game;
            }
            catch (MySqlException ex)
            {
                var messageBoxWindow = new MessageBoxWindow();
                var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                {
                    Title = "We ran into a problem",
                    Message = ex.Message,
                    Icon = PackIconKind.Error
                };
                messageBoxWindow.DataContext = viewModel;
                messageBoxWindow.ShowDialog();
                return null;
            }
        }


        public void AddPlayerStatistics(PlayerStatisticsModel playerStatistics)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"INSERT INTO IGRAC_STATISTIKA_UTAKMICA (TIM_STATISTIKA_UTAKMICA_TIM_naziv, TIM_STATISTIKA_UTAKMICA_UTAKMICA_idUtakmica, IGRAC_idIgrac 
                            , brojGolovaNaUtakmici, brojAsistencijaNaUtakmici, starter, zutiKarton, crveniKarton, odigraneMinuteNaUtakmici) VALUES 
                            (@TeamName, @GameId, @PlayerId, @Goals, @Assists, @Starter, @YellowCards, @RedCards, @MinutesPlayed)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamName", playerStatistics.TeamName);
                command.Parameters.AddWithValue("@GameId", playerStatistics.GameId);
                command.Parameters.AddWithValue("@PlayerId", playerStatistics.PlayerId);
                command.Parameters.AddWithValue("@Goals", playerStatistics.Goals);
                command.Parameters.AddWithValue("@Assists", playerStatistics.Assists);
                command.Parameters.AddWithValue("@Starter", playerStatistics.IsStarter);
                command.Parameters.AddWithValue("@YellowCards", playerStatistics.YellowCard);
                command.Parameters.AddWithValue("@RedCards", playerStatistics.RedCard);
                command.Parameters.AddWithValue("@MinutesPlayed", playerStatistics.MinutesPlayed);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<GamesModel> GetGames(string league, string season, int matchday)
        {
            ObservableCollection<GamesModel> games = new ObservableCollection<GamesModel>();

            string query = @"SELECT 
                                u.idUtakmica AS MatchId,
                                u.datumUtakmice AS MatchDate,
                                u.KOLO_brojKola AS Matchday,
                                u.KOLO_SEZONA_godina AS Season,
                                u.sudija AS Referee,
                                u.KOLO_SEZONA_LIGA_naziv AS League,
                                homeTeam.TIM_naziv AS HomeTeamName,
                                th.logo AS HomeTeamLogo,
                                homeTeam.brojPostignutihGolova AS HomeTeamGoals,
                                awayTeam.TIM_naziv AS AwayTeamName,
                                ta.logo AS AwayTeamLogo,
                                awayTeam.brojPostignutihGolova AS AwayTeamGoals
                            FROM UTAKMICA u
                            JOIN TIM_STATISTIKA_UTAKMICA homeTeam 
                                ON u.idUtakmica = homeTeam.UTAKMICA_idUtakmica AND homeTeam.jeDomacin = 1
                            LEFT JOIN TIM_STATISTIKA_UTAKMICA awayTeam 
                                ON u.idUtakmica = awayTeam.UTAKMICA_idUtakmica AND awayTeam.jeDomacin = 0
                            JOIN TIM th 
                                ON th.naziv = homeTeam.TIM_naziv 
                            LEFT JOIN TIM ta 
                                ON ta.naziv = awayTeam.TIM_naziv
                            WHERE 
                                u.KOLO_brojKola = @Matchday AND 
                                u.KOLO_SEZONA_godina = @Season AND 
                                u.KOLO_SEZONA_LIGA_naziv = @League;";

            using (var connection = new MySqlConnection(_connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Matchday", matchday);
                command.Parameters.AddWithValue("@Season", season);
                command.Parameters.AddWithValue("@League", league);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new GamesModel
                        {
                            Id = reader.GetInt32("MatchId"),
                            Date = reader.GetDateTime("MatchDate"),
                            Referee = reader.GetString("Referee"),
                            Matchday = reader.GetInt32("Matchday"),
                            Season = reader.GetString("Season"),
                            League = reader.GetString("League"),
                            HomeTeam = reader.GetString("HomeTeamName"),
                            HomeTeamLogo = (byte[])reader["HomeTeamLogo"],
                            HomeTeamGoals = reader.GetInt32("HomeTeamGoals"),
                            AwayTeam = reader.GetString("AwayTeamName"),
                            AwayTeamLogo = (byte[])reader["AwayTeamLogo"],
                            AwayTeamGoals = reader.GetInt32("AwayTeamGoals")
                        });
                    }
                }
            }

            return games;
        }

        public ObservableCollection<PlayerStatisticsModel> GetPlayerStatistics(string teamName, int gameId)
        {
            ObservableCollection<PlayerStatisticsModel> statistics = new ObservableCollection<PlayerStatisticsModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM IGRAC_STATISTIKA_UTAKMICA JOIN IGRAC ON idIgrac = IGRAC_idIgrac 
                                WHERE TIM_STATISTIKA_UTAKMICA_TIM_naziv = @TeamName AND TIM_STATISTIKA_UTAKMICA_UTAKMICA_idUtakmica = @GameId";
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@TeamName", teamName);
                command.Parameters.AddWithValue("@GameId", gameId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statistics.Add(new PlayerStatisticsModel
                        {
                            TeamName = teamName,
                            GameId = gameId,
                            PlayerId = (int)reader["IGRAC_idIgrac"],
                            Goals = (int)reader["brojGolovaNaUtakmici"],
                            Assists = (int)reader["brojAsistencijaNaUtakmici"],
                            MinutesPlayed = (int)reader["odigraneMinuteNaUtakmici"],
                            YellowCard = (bool)reader["zutiKarton"],
                            IsStarter = (bool)reader["starter"],
                            RedCard = (bool)reader["crveniKarton"],
                            Name = reader["ime"].ToString(),
                            Surname = reader["prezime"].ToString()
                        });
                    }
                }
            }

            return statistics;
        }
    }
}
