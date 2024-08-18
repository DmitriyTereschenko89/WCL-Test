using System.Windows.Controls;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl.CustomControls
{
    /// <summary>
    /// Interaction logic for UserLoginControl.xaml
    /// </summary>
    public partial class UserLoginControl : UserControl
    {
        public UserLoginControl(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
        }
    }
}
