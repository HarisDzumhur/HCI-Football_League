using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool ValidateUser(string username, string password);
        UserModel GetUserByUsername(string username);
        UserModel AddUser(UserModel user);
        bool IsUsernameTaken(string username);
        void UpdateUser(UserModel user);
        void SetUserPromotion(int userId, bool isAdministrator);
        void SetUserStatus(int userId, bool isBlocked);
        ObservableCollection<UserModel> GetAllUsers();
    }
}
