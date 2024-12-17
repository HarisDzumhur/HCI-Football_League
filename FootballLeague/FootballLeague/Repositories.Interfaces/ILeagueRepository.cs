using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        void AddLeague(string leagueName);
        ObservableCollection<string> GetAllLeagues();

        void AddSeason(string year, string leagueName);
        ObservableCollection<string> GetAllSeasonsByLeague(string leagueName);

        void AddMatchday(int matchdayNumber, string year, string leagueName);
        ObservableCollection<int> GetAllMatchdaysBySeason(string leagueName, string year);


    }
}
