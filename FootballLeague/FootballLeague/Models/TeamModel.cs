using FootballLeague.Core;
using Google.Protobuf.WellKnownTypes;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FootballLeague.Models
{
    public class TeamModel : ObservableObject
    {
        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string _coach;
        public string Coach { get => _coach; set { _coach = value; OnPropertyChanged(); } }

        private string _stadium;
        public string Stadium { get => _stadium; set { _stadium = value; OnPropertyChanged(); } }

        private string _city;
        public string City { get => _city; set { _city = value; OnPropertyChanged(); } }

        private int _trophiesCount;
        public int TrophiesCount { get => _trophiesCount; set { _trophiesCount = value; OnPropertyChanged(); } }

        private DateTime _establishmentDate;
        public DateTime EstablishmentDate { get => _establishmentDate; set { _establishmentDate = value; OnPropertyChanged(); } }

        private byte[] _logo;
        public byte[] Logo
        {
            get => _logo;
            set { _logo = value; OnPropertyChanged(); }
        }

        private PackIconKind _bellIcon = PackIconKind.BellOutline;
        public PackIconKind BellIcon
        {
            get => _bellIcon;
            set { _bellIcon = value; OnPropertyChanged(); }
        }

        private PackIconKind _playersIcon = PackIconKind.PersonGroup;
        public PackIconKind PlayersIcon
        {
            get => _playersIcon;
            set { _playersIcon = value; OnPropertyChanged(); }
        }

        private PackIconKind _playersIconAdd = PackIconKind.GroupAdd;
        public PackIconKind PlayersIconAdd
        {
            get => _playersIconAdd;
            set { _playersIconAdd = value; OnPropertyChanged(); }
        }

        private PackIconKind _deleteIcon = PackIconKind.Delete;
        public PackIconKind DeleteIcon
        {
            get => _deleteIcon;
            set { _deleteIcon = value; OnPropertyChanged(); }
        }
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is TeamModel team)
            {
                return this.Name == team.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
