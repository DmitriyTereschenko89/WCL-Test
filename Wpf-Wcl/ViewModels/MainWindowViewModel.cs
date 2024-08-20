using System.Collections.ObjectModel;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Wpf_Wcl.Common;
using Wpf_Wcl.CustomControls;
using Wpf_Wcl.Data_Transfer_Objects;
using Wpf_Wcl.Views;
using Cmd = Wpf_Wcl.Commands;

namespace Wpf_Wcl.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly string _defaultInformation = "Please sign in or sign up.";
        private readonly string _informationAboutProgrammer = "Dmitriy Tereshchenko\r\n.Net Developer\r\n• Have 4+ years experience in working with .Net technologies;\r\n• Good understanding of programming practices, OOP principles and design patterns;\r\n• Experienced in working with messaging queues such as RabbitMQ;\r\n• Experienced in MS SQL, Entity Framework and other related database technologies;\r\n• Motivated and willing to develop person and always try to go out personal boundaries;\r\n• Experienced in a product company in the education domain;\r\n• Passionate about LeetCode, HackerRank etc. Solved 700+ tasks on LeetCode and 300+ on other\r\nresources;";
        private readonly string _visible = "Visible";
        private readonly string _hidden = "Hidden";
        private readonly Dictionary<int, InformationControl> _informations;
        private readonly Dictionary<InformationControl, InformationViewModel> _informationViewModels;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly RegistrationViewModel _registrationViewModel;
        private readonly LoginViewModel _loginViewModel;
        private InformationControl _selectedInformation;
        private string _seachName;

        private void ChangeInformation(object sender)
        {
            int indexView = int.Parse(((MenuItemViewModel)sender).Header.Split(':')[^1]);
            if (_informations.TryGetValue(indexView, out InformationControl control))
            {
                SelectedInformation = control;
            }
        }

        private void Registration()
        {
            var registrationWindow = new RegistrationWindow(_registrationViewModel);
            _registrationViewModel.CloseWindow = new Cmd.RelayCommand((param) => CloseWindow(registrationWindow));

            if ((bool)!registrationWindow.ShowDialog())
            {
                _ = Login(_registrationViewModel.UserName, _registrationViewModel.Password);
            }

        }

        private void ChangeAuth(string visibleLogin, string visibleLogout, UserInformationDto user = null)
        {
            _loginViewModel.VisibleLogin = visibleLogin;
            _loginViewModel.VisibleLogout = visibleLogout;
            if (user is null)
            {
                _informationViewModels[_selectedInformation].Information = _defaultInformation;
                return;
            }

            _informationViewModels[_selectedInformation].Information = $"id:{user.Id}\r\nfirst name: {user.FirstName}\r\nlast name:{user.LastName}\r\nemail: {user.Email}\r\nphone number:{user.Phone}";
        }

        private void AddNewView()
        {
            int newViewNum = MenuItems[1].Submenu.Count + 1;
            var lastViewModel = new InformationViewModel();
            _informations[newViewNum] = new InformationControl(lastViewModel);
            _informationViewModels[_informations[newViewNum]] = lastViewModel;
            var newMenuItem = new MenuItemViewModel($"View:{newViewNum}");
            var command = new Cmd.RelayCommand((param) => ChangeInformation(newMenuItem));
            newMenuItem.Command = command;
            MenuItems[1].Submenu.Add(newMenuItem);
        }

        private async Task Login(string username = null, SecureString password = null)
        {
            var user = await _apiService.LoginAsync(new NetworkCredential(username ?? _loginViewModel.UserName, password ?? _loginViewModel.Password));
            if (user != null)
            {
                ChangeAuth(_hidden, _visible, _mapper.Map<UserInformationDto>(user));
                return;
            }

            _ = MessageBox.Show("Incorrect login or password.");
        }

        private async Task Logout()
        {
            await _apiService.LogoutAsync();
            ChangeAuth(_visible, _hidden);
        }

        private async Task Search()
        {
            var user = await _apiService.GetUserAsync(_seachName);
            if (user is null)
            {
                _informationViewModels[_selectedInformation].Information = "Not found.";
                return;
            }

            _informationViewModels[_selectedInformation].Information = $"id:{user.Id}\r\nfirst name: {user.FirstName}\r\nlast name:{user.LastName}\r\nemail: {user.Email}\r\nphone number:{user.Phone}";
        }

        private void CloseWindow(Window window) => window.Close();

        public ICommand RegistrationCommand { get; set; }
        public IAsyncRelayCommand SearchCommand { get; set; }
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public UserLoginControl UserLoginControl { get; set; }

        public MainWindowViewModel(IApiService apiService, IMapper mapper, LoginViewModel loginViewModel, RegistrationViewModel registrationViewModel)
        {
            _apiService = apiService;
            _mapper = mapper;
            _informations = [];
            _informationViewModels = [];
            var firstViewModel = new InformationViewModel(_defaultInformation);
            var secondViewModel = new InformationViewModel(_informationAboutProgrammer);
            _informations[1] = new InformationControl(firstViewModel);
            _informations[2] = new InformationControl(secondViewModel);
            _informationViewModels[_informations[1]] = firstViewModel;
            _informationViewModels[_informations[2]] = secondViewModel;
            _loginViewModel = loginViewModel;
            _loginViewModel.LoginCommand = new AsyncRelayCommand((param) => Login());
            _loginViewModel.LogoutCommand = new AsyncRelayCommand((param) => Logout());
            UserLoginControl = new UserLoginControl(loginViewModel);
            _registrationViewModel = registrationViewModel;
            RegistrationCommand = new Cmd.RelayCommand((param) => Registration());
            SearchCommand = new AsyncRelayCommand((param) => Search());
            SelectedInformation = _informations[1];
            MenuItems = [
                new("Add", null, [new("View", new Cmd.RelayCommand((param) => AddNewView()))]),
                new("Views", null, [new("View:1"), new("View:2")])
            ];

            MenuItems[1].Submenu[0].Command = new Cmd.RelayCommand((param) => ChangeInformation(MenuItems[1].Submenu[0]));
            MenuItems[1].Submenu[1].Command = new Cmd.RelayCommand((param) => ChangeInformation(MenuItems[1].Submenu[1]));
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

        public string SearchName
        {
            get => _seachName;
            set
            {
                _seachName = value;
                OnPropertyChanged(nameof(SearchName));
            }
        }
    }
}
