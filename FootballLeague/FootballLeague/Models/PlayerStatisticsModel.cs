using FootballLeague.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class PlayerStatisticsModel : ObservableObject
    {
        private int _gameId;
        public int GameId { get => _gameId; set {  _gameId = value; OnPropertyChanged(); } }

        private int _playerId;
        public int PlayerId { get => _playerId; set { _playerId = value; OnPropertyChanged(); } }

        private string _teamName;
        public string TeamName { get => _teamName; set { _teamName = value; OnPropertyChanged(); } }

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string _surname;
        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); } }

        private int _goals;
        public int Goals { get => _goals; set { _goals = value; OnPropertyChanged(); } }

        private int _assists;
        public int Assists { get => _assists; set { _assists = value; OnPropertyChanged(); } }

        private int _minutesPlayed;
        public int MinutesPlayed { get => _minutesPlayed; set { _minutesPlayed = value; OnPropertyChanged(); } }

        private bool _isStarter;
        public bool IsStarter { get => _isStarter; set { _isStarter = value; OnPropertyChanged(); } }

        private bool _yellowCard;
        public bool YellowCard { get => _yellowCard; set { _yellowCard = value; OnPropertyChanged(); } }

        private bool _redCard;
        public bool RedCard { get => _redCard; set { _redCard = value; OnPropertyChanged(); } }

    }
}
