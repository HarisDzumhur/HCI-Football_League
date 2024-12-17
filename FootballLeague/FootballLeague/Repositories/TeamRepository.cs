using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly string _connectionString;
        public TeamRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public PlayerModel AddPlayer(PlayerModel player)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"INSERT INTO IGRAC (brojDresa, brojZutihKartona, brojCrvenihKartona, odigraneMinute, brojGolova, brojAsistencija, ime, prezime, brojGodina, TIM_naziv) " +
                    "VALUES (@KitNumber, @YellowCards, @RedCards, @MinutesPlayed, @Goals, @Assists, @Name, @Surname, @Age, @TeamName);" +
                    "SELECT LAST_INSERT_ID();";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@KitNumber", player.KitNumber);
                command.Parameters.AddWithValue("@YellowCards", player.YellowCards);
                command.Parameters.AddWithValue("@RedCards", player.RedCards);
                command.Parameters.AddWithValue("@MinutesPlayed", player.MinutesPlayed);
                command.Parameters.AddWithValue("@Goals", player.Goals);
                command.Parameters.AddWithValue("@Assists", player.Assists);
                command.Parameters.AddWithValue("@Name", player.PlayerName);
                command.Parameters.AddWithValue("@Surname", player.PlayerSurname);
                command.Parameters.AddWithValue("@Age", player.Age);
                command.Parameters.AddWithValue("@TeamName", player.TeamName);

                connection.Open();
                var playerId = Convert.ToInt32(command.ExecuteScalar());
                player.Id = playerId;
                return player;
            }
        }

        public void AddTeam(TeamModel team)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var checkQuery = "SELECT COUNT(*) FROM TIM WHERE naziv = @Name AND jeObrisan = 1";
                var checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Name", team.Name);

                var existingDeletedCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingDeletedCount > 0)
                {
                    var updateQuery = @"UPDATE TIM  SET jeObrisan = 0, brojTrofeja = @TrophiesCount, stadion = @Stadium,  trener = @Coach, datumOsnivanja = @EstablishmentDate, grad = @City, logo = @Logo
                                        WHERE naziv = @Name";
                    var updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Name", team.Name);
                    updateCommand.Parameters.AddWithValue("@TrophiesCount", team.TrophiesCount);
                    updateCommand.Parameters.AddWithValue("@Stadium", team.Stadium);
                    updateCommand.Parameters.AddWithValue("@Coach", team.Coach);
                    updateCommand.Parameters.AddWithValue("@EstablishmentDate", team.EstablishmentDate);
                    updateCommand.Parameters.AddWithValue("@City", team.City);
                    updateCommand.Parameters.AddWithValue("@Logo", team.Logo);

                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    var insertQuery = @"INSERT INTO TIM (naziv, jeObrisan, brojTrofeja, stadion, trener, datumOsnivanja, grad, logo)
                                        VALUES (@Name, 0, @TrophiesCount, @Stadium, @Coach, @EstablishmentDate, @City, @Logo)";
                    var insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Name", team.Name);
                    insertCommand.Parameters.AddWithValue("@TrophiesCount", team.TrophiesCount);
                    insertCommand.Parameters.AddWithValue("@Stadium", team.Stadium);
                    insertCommand.Parameters.AddWithValue("@Coach", team.Coach);
                    insertCommand.Parameters.AddWithValue("@EstablishmentDate", team.EstablishmentDate);
                    insertCommand.Parameters.AddWithValue("@City", team.City);
                    insertCommand.Parameters.AddWithValue("@Logo", team.Logo);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        public ObservableCollection<PlayerModel> GetAllPlayersByTeam(string teamName)
        {
            ObservableCollection<PlayerModel> players = new ObservableCollection<PlayerModel>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM IGRAC WHERE TIM_naziv = @TeamName";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamName", teamName);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!(bool)reader["jeObrisan"])
                            players.Add(new PlayerModel
                            {
                                Id = (int)reader["idIgrac"],
                                KitNumber = (int)reader["brojDresa"],
                                YellowCards = (int)reader["brojZutihKartona"],
                                RedCards = (int)reader["brojCrvenihKartona"],
                                MinutesPlayed = (int)reader["odigraneMinute"],
                                Goals = (int)reader["brojGolova"],
                                Assists = (int)reader["brojAsistencija"],
                                Age = (int)reader["brojGodina"],
                                PlayerName = reader["ime"].ToString(),
                                PlayerSurname = reader["prezime"].ToString(),
                                TeamName = reader["TIM_naziv"].ToString(),
                            });
                    }
                }
            }

            return players;
        }

        public ObservableCollection<TeamModel> GetAllTeams()
        {
            ObservableCollection<TeamModel> teams = new ObservableCollection<TeamModel>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM TIM";
                var command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!(bool)reader["jeObrisan"])
                            teams.Add(new TeamModel
                            {
                                Name = reader["naziv"].ToString(),
                                Coach = reader["trener"].ToString(),
                                Stadium = reader["stadion"].ToString(),
                                City = reader["grad"].ToString(),
                                EstablishmentDate = reader.GetDateTime(reader.GetOrdinal("datumOsnivanja")),
                                Logo = (byte[])reader["logo"],
                                TrophiesCount = (int)reader["brojTrofeja"]
                            });
                    }
                }
            }

            return teams;
        }

        public void RemovePlayer(int playerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"UPDATE IGRAC SET jeObrisan = 1 WHERE idIgrac = @PlayerId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerId", playerId);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void RemoveTeam(string teamName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"UPDATE TIM SET jeObrisan = 1 WHERE naziv = @TeamName";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeamName", teamName);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
