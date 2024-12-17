using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BCrypt.Net;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Service;

namespace FootballLeague.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }


        public bool ValidateUser(string username, string password)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT lozinka FROM KORISNIK WHERE korisnickoIme = @Username";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                var storedHash = command.ExecuteScalar()?.ToString();

                if (storedHash != null)
                {
                    return BCrypt.Net.BCrypt.Verify(password, storedHash);
                }

                return false;
            }
        }


        public UserModel GetUserByUsername(string username)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM KORISNIK WHERE korisnickoIme = @Username";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserModel
                        {
                            Id = (int)reader["idKorisnik"],
                            Username = reader["korisnickoIme"].ToString(),
                            Name = reader["ime"].ToString(),
                            Surname = reader["prezime"].ToString(),
                            Password = reader["lozinka"].ToString(),
                            IsBlocked = (bool)reader["jeBlokiran"],
                            SettingsId = (int)reader["POSTAVKE_idPostavke"],
                            IsAdministrator = (bool)reader["jeAdministrator"]
                        };
                    }
                }
            }
            return null;
        }

        public UserModel AddUser(UserModel user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"INSERT INTO KORISNIK (korisnickoIme, ime, prezime, lozinka, jeBlokiran, POSTAVKE_idPostavke, jeAdministrator) 
                          VALUES (@Username, @Name, @Surname, @Password, @IsBlocked, @SettingsId, @IsAdministrator);
                          SELECT LAST_INSERT_ID();";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Surname", user.Surname);
                command.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(user.Password));
                command.Parameters.AddWithValue("@IsBlocked", user.IsBlocked);
                command.Parameters.AddWithValue("@SettingsId", user.SettingsId);
                command.Parameters.AddWithValue("@IsAdministrator", user.IsAdministrator);

                connection.Open();

                var userId = Convert.ToInt32(command.ExecuteScalar());
                user.Id = userId;
                return user;
            }
        }

        public bool IsUsernameTaken(string username)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"SELECT COUNT(*) FROM KORISNIK WHERE korisnickoIme = @Username";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();

                return (long)command.ExecuteScalar() > 0;
            }
        }

        public void UpdateUser(UserModel user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"UPDATE KORISNIK SET korisnickoIme = @Username, ime = @Name, prezime = @Surname, lozinka = @Password, jeBlokiran = @IsBlocked, POSTAVKE_idPostavke = @SettingsId,
                            jeAdministrator = @IsAdministrator WHERE idKorisnik = @Id";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Surname", user.Surname);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsBlocked", user.IsBlocked);
                command.Parameters.AddWithValue("@IsAdministrator", user.IsAdministrator);
                command.Parameters.AddWithValue("@SettingsId", user.SettingsId);
                command.Parameters.AddWithValue("@Id", user.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SetUserPromotion(int userId, bool isAdministrator)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"UPDATE KORISNIK SET jeAdministrator = @IsAdministrator WHERE idKorisnik = @UserId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@IsAdministrator", isAdministrator);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SetUserStatus(int userId, bool isBlocked)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = @"UPDATE KORISNIK SET jeBlokiran = @IsBlocked WHERE idKorisnik = @UserId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@IsBlocked", isBlocked);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<UserModel> GetAllUsers()
        {
            ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM KORISNIK";
                var command = new MySqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Id = (int)reader["idKorisnik"],
                            Username = reader["korisnickoIme"].ToString(),
                            Name = reader["ime"].ToString(),
                            Surname = reader["prezime"].ToString(),
                            Password = reader["lozinka"].ToString(),
                            IsBlocked = (bool)reader["jeBlokiran"],
                            SettingsId = (int)reader["POSTAVKE_idPostavke"],
                            IsAdministrator = (bool)reader["jeAdministrator"]
                        });
                    }
                }
            }
            return users;
        }
    }
}
