using FootballLeague.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class ResultModel : ObservableObject
    {
        private DateTime _date;
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(); } }

        private int _homeTeamGoals;
        public int HomeTeamGoals{ get => _homeTeamGoals; set { _homeTeamGoals = value; OnPropertyChanged(); Score = $"{value} - {AwayTeamGoals}"; } }

        private int _awayTeamGoals;
        public int AwayTeamGoals { get => _awayTeamGoals; set { _awayTeamGoals = value; OnPropertyChanged(); Score = $"{HomeTeamGoals} - {value}"; } }

        private string _awayTeamName;
        public string AwayTeamName { get => _awayTeamName; set { _awayTeamName = value; OnPropertyChanged(); } }

        private byte[] _awayTeamLogo;
        public byte[] AwayTeamLogo { get => _awayTeamLogo; set { _awayTeamLogo = value; OnPropertyChanged(); } }

        private string _score;
        public string Score { get => _score; set { _score = value; OnPropertyChanged(); } }
    }
}
