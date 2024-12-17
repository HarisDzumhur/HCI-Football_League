using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballLeague.Service
{
    public class DialogService : IDialogService
    {
        private readonly Window _window;

        public DialogService(Window window)
        {
            _window = window;
        }

        public void CloseDialog()
        {
            _window.Close();
        }
    }
}
