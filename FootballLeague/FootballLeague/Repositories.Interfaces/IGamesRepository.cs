using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface IGamesRepository
    {
        ObservableCollection<GamesModel> GetGames(string league, string season, int matchday);
        GamesModel AddGame(GamesModel game);
        void AddPlayerStatistics(PlayerStatisticsModel playerStatistics);
        ObservableCollection<PlayerStatisticsModel> GetPlayerStatistics(string teamName, int gameId);

    }
}
