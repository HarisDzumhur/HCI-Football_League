using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        SettingsModel GetUserSettings(int settingsId);
        int GetSettingsId(string theme, string font, string language);
        ObservableCollection<string> GetEnumValues(string column);
    }
}
