using System.Collections.ObjectModel;
using System.Windows;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl.UserControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            MenuItems =
            [
                new() {Header = "Add", MenuItems = [new() { Header = "View" }]},
                new() {Header = "Views", MenuItems = [new() { Header = "View1" }, new() { Header = "View2 "}]}
            ];

            DataContext = this;
        }
    }
}
