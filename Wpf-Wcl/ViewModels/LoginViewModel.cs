using System.Net;
using System.Security;
using CommunityToolkit.Mvvm.Input;
using Wpf_Wcl.Common;

namespace Wpf_Wcl.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _userName;
        private SecureString _password;
        private readonly IApiService _apiService;

        private async Task Login()
        {
            //await Task.Delay(10000);
            var user = await _apiService.LoginAsync(new NetworkCredential(_userName, _password));
        }

        public LoginViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoginCommand = new AsyncRelayCommand((param) => Login());
        }
        public IAsyncRelayCommand LoginCommand { get; set; }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
