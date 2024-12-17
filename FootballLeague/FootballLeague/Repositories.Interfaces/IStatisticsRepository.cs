using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        ObservableCollection<StatisticsModel> GetAllTeams(string year, string leagueName);
    }
}
