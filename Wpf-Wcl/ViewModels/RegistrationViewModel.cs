using System.Security;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Wpf_Wcl.Common;
using Wpf_Wcl.Models;

namespace Wpf_Wcl.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly IApiService _apiService;

        private async Task Registration()
        {
            var user = await _apiService.RegisterAsync(_user);
        }

        public RegistrationViewModel(IApiService apiService)
        {
            _user = new();
            _apiService = apiService;
            RegistrationCommand = new AsyncRelayCommand((param) => Registration());
        }

        public IAsyncRelayCommand RegistrationCommand { get; set; }

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
            get => _user.PhoneNumber;
            set
            {
                _user.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
    }
}
