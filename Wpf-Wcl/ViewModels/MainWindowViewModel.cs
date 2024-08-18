using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf_Wcl.Commands;
using Wpf_Wcl.Common;
using Wpf_Wcl.CustomControls;
using Wpf_Wcl.Views;

namespace Wpf_Wcl.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly string _informationAboutProgrammer = "Dmitriy Tereshchenko\r\n.Net Developer\r\n• Have 4+ years experience in working with .Net technologies;\r\n• Good understanding of programming practices, OOP principles and design patterns;\r\n• Experienced in working with messaging queues such as RabbitMQ;\r\n• Experienced in MS SQL, Entity Framework and other related database technologies;\r\n• Motivated and willing to develop person and always try to go out personal boundaries;\r\n• Experienced in a product company in the education domain;\r\n• Passionate about LeetCode, HackerRank etc. Solved 700+ tasks on LeetCode and 300+ on other\r\nresources;";
        private readonly Dictionary<int, InformationControl> _informations;
        private readonly IApiService _apiService;
        private InformationControl _selectedInformation;
        private readonly RegistrationViewModel _registrationViewModel;

        private void ChangeInformation(object sender)
        {
            //var user = await _apiService.GetUserAsync("admin");
            int indexView = int.Parse(((MenuItemViewModel)sender).Header.Split(':')[^1]);
            if (_informations.TryGetValue(indexView, out InformationControl control))
            {
                SelectedInformation = control;
            }
        }

        private void Registration()
        {
            var registrationWindow = new RegistrationWindow(_registrationViewModel);
            _ = registrationWindow.ShowDialog();

        }

        private void AddNewView()
        {
            //int newViewNum = MenuItems[1].Submenu.Count + 1;
            //_informations[newViewNum] = new InformationControl();
            //MenuItems[1].Submenu.Add(new($"View:{newViewNum}"));
            //MenuItems[1].Submenu[^1].Command = new RelayCommand((param) => ChangeInformation(MenuItems[1].Submenu[^1]));
            int newViewNum = MenuItems[1].Submenu.Count + 1;
            _informations[newViewNum] = new InformationControl();
            var newMenuItem = new MenuItemViewModel($"View:{newViewNum}");
            var command = new RelayCommand((param) => ChangeInformation(newMenuItem));
            newMenuItem.Command = command;
            MenuItems[1].Submenu.Add(newMenuItem);

            // Set the command for the newly added view
            //MenuItems[1].Submenu[^1].Command = new RelayCommand((param) => ChangeInformation(MenuItems[1].Submenu[^1]));
        }

        public ICommand RegistrationCommand { get; set; }
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public UserLoginControl UserLoginControl { get; set; }

        public MainWindowViewModel(IApiService apiService, UserLoginControl userLoginControl, RegistrationViewModel registrationViewModel)
        {
            _apiService = apiService;
            _informations = [];
            _informations[1] = new InformationControl("Please sign in or sign up.");
            _informations[2] = new InformationControl(_informationAboutProgrammer);
            UserLoginControl = userLoginControl;
            _registrationViewModel = registrationViewModel;
            RegistrationCommand = new RelayCommand((param) => Registration());
            SelectedInformation = _informations[1];
            MenuItems = [
                new("Add", null, [new("View", new RelayCommand((param) => AddNewView()))]),
                new("Views", null, [new("View:1"), new("View:2")])
            ];

            MenuItems[1].Submenu[0].Command = new RelayCommand((param) => ChangeInformation(MenuItems[1].Submenu[0]));
            MenuItems[1].Submenu[^1].Command = new RelayCommand((param) => ChangeInformation(MenuItems[1].Submenu[^1]));
        }

        public InformationControl SelectedInformation
        {
            get => _selectedInformation;
            set
            {
                _selectedInformation = value;
                OnPropertyChanged(nameof(SelectedInformation));
            }
        }
    }
}
