using FootballLeague.Core;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class PlayerModel : ObservableObject
    {
        private int _kitNumber;
        private string _playerName;
        private string _playerSurname;
        private int _age;
        private int _minutesPlayed;
        private int _goals;
        private int _assists;
        private int _yellowCards;
        private int _redCards;
        private int _id;
        private string _teamName;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string TeamName
        {
            get => _teamName;
            set
            {
                _teamName = value;
                OnPropertyChanged();
            }
        }

        public int KitNumber
        {
            get => _kitNumber;
            set
            {
                _kitNumber = value;
                OnPropertyChanged();
            }
        }

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        public string PlayerSurname
        {
            get => _playerSurname;
            set
            {
                _playerSurname = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public int MinutesPlayed
        {
            get => _minutesPlayed;
            set
            {
                _minutesPlayed = value;
                OnPropertyChanged();
            }
        }

        public int Goals
        {
            get => _goals;
            set
            {
                _goals = value;
                OnPropertyChanged();
            }
        }

        public int Assists
        {
            get => _assists;
            set
            {
                _assists = value;
                OnPropertyChanged();
            }
        }

        public int YellowCards
        {
            get => _yellowCards;
            set
            {
                _yellowCards = value;
                OnPropertyChanged();
            }
        }

        public int RedCards
        {
            get => _redCards;
            set
            {
                _redCards = value;
                OnPropertyChanged();
            }
        }

        private PackIconKind _deleteIcon = PackIconKind.Delete;
        public PackIconKind DeleteIcon
        {
            get => _deleteIcon;
            set { _deleteIcon = value; OnPropertyChanged(); }
        }

        public override string ToString()
        {
            return $"{PlayerName} {PlayerSurname}";
        }
    }
}
