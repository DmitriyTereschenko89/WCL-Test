using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Wpf_Wcl.ViewModels
{
    public class MenuItemViewModel : VIewModelBase
    {
        public string Header { get; set; }

        public IAsyncRelayCommand Command { get; set; }

        public ObservableCollection<MenuItemViewModel> Submenu { get; set; }

        public MenuItemViewModel()
        {
            Submenu = [];
        }

        public MenuItemViewModel(string header, IAsyncRelayCommand command, ObservableCollection<MenuItemViewModel> submenu = null)
        {
            Header = header;
            Command = command;

            Submenu = submenu ?? [];
        }
    }
}
