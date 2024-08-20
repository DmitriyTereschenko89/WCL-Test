using System.Net;
using System.Security;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Wpf_Wcl.Common;
using Wpf_Wcl.Data_Transfer_Objects;
using Wpf_Wcl.Models;

namespace Wpf_Wcl.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly UserDto _user;
        private readonly IMapper _mapper;
        private readonly IApiService _apiService;

        private async Task<User> Registration()
        {
            var user = _mapper.Map<User>(_user);
            var netCredential = new NetworkCredential(_user.UserName, _user.Password);
            user.Username = netCredential.UserName;
            user.Password = netCredential.Password;
            var newUser = await _apiService.RegisterAsync(user);
            if (newUser != null)
            {
                CloseWindow.Execute(this);
                return newUser;
            }

            return null;
        }

        public RegistrationViewModel(IApiService apiService, IMapper mapper)
        {
            _user = new();
            _apiService = apiService;
            _mapper = mapper;
            RegistrationCommand = new AsyncRelayCommand((param) => Registration());
        }

        public IAsyncRelayCommand RegistrationCommand { get; set; }
        public ICommand CloseWindow { get; set; }

        public string UserName
        {
            get => _user.UserName;
            set
            {
                _user.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public SecureString Password
        {
            get => _user.Password;
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string FirstName
        {
            get => _user.FirstName;
            set
            {
                _user.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _user.LastName;
            set
            {
                _user.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                _user.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string PhoneNumber
        {
            get => _user.Phone;
            set
            {
                _user.Phone = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
    }
}
