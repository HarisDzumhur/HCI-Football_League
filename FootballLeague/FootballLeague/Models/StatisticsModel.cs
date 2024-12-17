using FootballLeague.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class StatisticsModel : ObservableObject
    {
        private int position;
        public int Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }
        
        private byte[] logo;
        public byte[] Logo
        {
            get => logo;
            set
            {
                logo = value;
                OnPropertyChanged();
            }
        }

        private string season;
        public string Season
        {
            get => season;
            set
            {
                season = value;
                OnPropertyChanged();
            }
        }

        private string league;
        public string League
        {
            get => league;
            set
            {
                league = value;
                OnPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private int gamesPlayed;
        public int GamesPlayed
        {
            get => gamesPlayed;
            set
            {
                gamesPlayed = value;
                OnPropertyChanged();
            }
        }

        private int wins;
        public int Wins
        {
            get => wins;
            set
            {
                wins = value;
                OnPropertyChanged();
            }
        }

        private int loses;
        public int Loses
        {
            get => loses;
            set
            {
                loses = value;
                OnPropertyChanged();
            }
        }

        private int draws;
        public int Draws
        {
            get => draws;
            set
            {
                draws = value;
                OnPropertyChanged();
            }
        }

        private int goalsScored;
        public int GoalsScored
        {
            get => goalsScored;
            set
            {
                goalsScored = value;
                OnPropertyChanged();
            }
        }

        private int goalsConceeded;
        public int GoalsConceeded
        {
            get => goalsConceeded;
            set
            {
                goalsConceeded = value;
                OnPropertyChanged();
            }
        }

        private int goalDifference;
        public int GoalDifference
        {
            get => goalDifference;
            set
            {
                goalDifference = value;
                OnPropertyChanged();
            }
        }

        private int points;
        public int Points
        {
            get => points;
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }
    }
}
