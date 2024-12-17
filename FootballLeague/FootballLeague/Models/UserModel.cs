using FootballLeague.Core;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class UserModel : ObservableObject
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged(); } }

        private string _username;
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private string _surname;
        public string Surname { get { return _surname; } set { _surname = value; OnPropertyChanged(); } }

        private string _plainPassword;
        public string PlainPassword { get { return _plainPassword; } set { _plainPassword = value; OnPropertyChanged(); } }

        private string _password;
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }

        private bool _isBlocked;
        public bool IsBlocked { get => _isBlocked; 
            set { _isBlocked = value; 
                OnPropertyChanged(); 
                BlockIcon = IsBlocked ? PackIconKind.PersonBlock : PackIconKind.PersonBlockOutline;
            } }

        private bool _isAdministrator;
        public bool IsAdministrator { get => _isAdministrator; 
            set { _isAdministrator = value; OnPropertyChanged(); PromoteIcon = IsAdministrator ? PackIconKind.Crown : PackIconKind.CrownOutline; } }

        private int _settingsId;
        public int SettingsId { get => _settingsId; set { _settingsId = value; OnPropertyChanged(); } }

        private PackIconKind _blockIcon;
        public PackIconKind BlockIcon
        {
            get => _blockIcon;
            set { _blockIcon = value; OnPropertyChanged(); }
        }

        private PackIconKind _promoteIcon;
        public PackIconKind PromoteIcon
        {
            get => _promoteIcon;
            set { _promoteIcon = value; OnPropertyChanged(); }
        }

        public override bool Equals(object obj)
        {
            if (obj is UserModel user)
            {
                return this.Id == user.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
