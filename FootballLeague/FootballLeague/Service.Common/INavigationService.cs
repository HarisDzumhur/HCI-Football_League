using FootballLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Service.Common
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<T>() where T : ViewModelBase;
        void NavigateTo<T>(object parameters) where T : ViewModelBase;
        ViewModelBase DynamicView { get; }
        void NavigateToDynamicView<T>() where T : ViewModelBase;
        public void NavigateToDynamicView<T>(object parameter) where T : ViewModelBase;
    }
}
