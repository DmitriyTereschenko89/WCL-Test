using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf_Wcl.Commands;

namespace Wpf_Wcl.ViewModels
{
    public class MenuItemViewModel : VIewModelBase
    {
        public MenuItemViewModel()
        {
            Command = new RelayCommand((param) => Execute());
        }

        public string Header { get; set; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public ICommand Command { get; }

        private void Execute()
        {
            _ = MessageBox.Show("Clicked at " + Header);
        }
    }
}
