using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Wpf_Wcl.ViewModels
{
    public class MenuItemViewModel : ViewModelBase
    {
        public string Header { get; set; }

        public ICommand Command { get; set; }

        public ObservableCollection<MenuItemViewModel> Submenu { get; set; }

        public MenuItemViewModel()
        {
            Submenu = [];
        }

        public MenuItemViewModel(string header, ICommand command = null, ObservableCollection<MenuItemViewModel> submenu = null)
        {
            Header = header;
            Command = command;

            Submenu = submenu ?? [];
        }
    }
}
