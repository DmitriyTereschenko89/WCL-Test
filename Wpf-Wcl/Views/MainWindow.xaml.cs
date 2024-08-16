using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using Wpf_Wcl.Common;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl.UserControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public UserLoginControl UserLoginControl { get; set; }
        public static readonly DependencyProperty ViewControlInformation = DependencyProperty.Register("SelectedPage", typeof(ViewControl), typeof(MainWindow));

        public MainWindow(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            Views = [];
            Views[1] = new ViewControl("Please log in or sign up!");
            Views[2] = new ViewControl("Information about me!");
            UserLoginControl = new UserLoginControl();
            SelectedPage = Views[1];
            MenuItems = [
                new("Add", null, [new("View", new AsyncRelayCommand(async (param) =>  await AddNewViewAsync()))]),
                new("Views", null, [new("View:1", null), new("View:2", null)])
            ];

            MenuItems[1].Submenu[0].Command = new AsyncRelayCommand(async (param) => await ChangeViewASync(MenuItems[1].Submenu[0]));
            MenuItems[1].Submenu[1].Command = new AsyncRelayCommand(async (param) => await ChangeViewASync(MenuItems[1].Submenu[1]));

            DataContext = this;
        }

        public ViewControl SelectedPage
        {
            get => Dispatcher.Invoke(() => (ViewControl)GetValue(ViewControlInformation));
            set => Dispatcher.Invoke(() => SetValue(ViewControlInformation, value));
        }

        private Dictionary<int, ViewControl> Views { get; }
        private readonly IApiService _apiService;

        private async Task ChangeViewASync(object sender)
        {
            //var user = await _apiService.GetUserAsync("admin");
            await Task.Run(() =>
            {
                int indexView = int.Parse(((MenuItemViewModel)sender).Header.Split(':')[^1]);
                if (Views.TryGetValue(indexView, out ViewControl control))
                {
                    SelectedPage = control;
                }
            });
        }

        private async Task AddNewViewAsync()
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    int newViewNum = MenuItems[1].Submenu.Count + 1;
                    Views[newViewNum] = new ViewControl();
                    MenuItems[1].Submenu.Add(new($"View:{newViewNum}", null));
                    MenuItems[1].Submenu[^1].Command = new AsyncRelayCommand(async (param) => await ChangeViewASync(MenuItems[1].Submenu[^1]));
                });
            });
        }
    }
}
