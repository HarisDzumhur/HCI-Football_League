using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Service
{
    public class UserService
    {
        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        private SettingsModel _currentSettings;
        public SettingsModel CurrentSettings
        {
            get => _currentSettings;
            set => _currentSettings = value;
        }
    }
}
