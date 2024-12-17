using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        ObservableCollection<TeamModel> GetAllTeams();
        void AddTeam(TeamModel team);
        ObservableCollection<PlayerModel> GetAllPlayersByTeam(string teamName);
        void RemoveTeam(string teamName);
        PlayerModel AddPlayer(PlayerModel player);
        void RemovePlayer(int playerId);
    }
}
