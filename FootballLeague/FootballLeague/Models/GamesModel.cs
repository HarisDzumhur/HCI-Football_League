using FootballLeague.Core;
using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballLeague.Models
{
    public class GamesModel : ObservableObject
    {
        private int _id;
        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }

        private DateTime _date;
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(); } }

        private int _matchday;
        public int Matchday { get => _matchday; set { _matchday = value; OnPropertyChanged(); } }

        private string _season;
        public string Season { get => _season; set { _season = value; OnPropertyChanged(); } }

        private string _league;
        public string League { get => _league; set { _league = value; OnPropertyChanged(); } }

        private string _referee;
        public string Referee { get => _referee; set { _referee = value; OnPropertyChanged(); } }

        private string _homeTeam;
        public string HomeTeam { get => _homeTeam; set { _homeTeam = value; OnPropertyChanged(); } }

        private int _homeTeamGoals;
        public int HomeTeamGoals { get => _homeTeamGoals; set { _homeTeamGoals = value; OnPropertyChanged(); Result = $"{value} : {AwayTeamGoals}"; } }

        private byte[] _homeTeamLogo;
        public byte[] HomeTeamLogo
        {
            get => _homeTeamLogo;
            set { _homeTeamLogo = value; OnPropertyChanged(); }
        }

        private string _awayTeam;
        public string AwayTeam { get => _awayTeam; set { _awayTeam = value; OnPropertyChanged(); } }

        private int _awayTeamGoals;
        public int AwayTeamGoals { get => _awayTeamGoals; set { _awayTeamGoals = value; OnPropertyChanged(); Result = $"{HomeTeamGoals} : {value}"; } }

        private byte[] _awayTeamLogo;
        public byte[] AwayTeamLogo
        {
            get => _awayTeamLogo;
            set { _awayTeamLogo = value; OnPropertyChanged(); }
        }

        private string _result;
        public string Result { get => _result; set { _result = value; OnPropertyChanged(); } }

        private PackIconKind _details = PackIconKind.ChartLine;
        public PackIconKind Details
        {
            get => _details;
            set { _details = value; OnPropertyChanged(); }
        }
    }
}
