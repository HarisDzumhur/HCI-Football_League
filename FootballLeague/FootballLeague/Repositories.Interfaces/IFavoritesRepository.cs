using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        void AddFollow(string teamName, int playerId);
        void RemoveFollow(string teamName, int playerId);
        List<string> GetFollowsByUser(int playerId);
        ObservableCollection<FavouritesModel> GetFavouritesByUser(int playerId);
    }
}
