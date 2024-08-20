using System.Security;
using CommunityToolkit.Mvvm.Input;
using Wpf_Wcl.Common;

namespace Wpf_Wcl.ViewModels
{
    public class LoginViewModel(string visibleLogin = "Visible", string visibleLogout = "Hidden") : ViewModelBase
    {
        private string _userName;
        private SecureString _password;
        private string _errorMessage;
        private string _visibleLogin = visibleLogin;
        private string _visibleLogout = visibleLogout;

        public IAsyncRelayCommand LoginCommand { get; set; }
        public IAsyncRelayCommand LogoutCommand { get; set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string VisibleLogin
        {
            get => _visibleLogin;
            set
            {
                _visibleLogin = value;
                OnPropertyChanged(nameof(VisibleLogin));
            }
        }

        public string VisibleLogout
        {
            get => _visibleLogout;
            set
            {
                _visibleLogout = value;
                OnPropertyChanged(nameof(VisibleLogout));
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
