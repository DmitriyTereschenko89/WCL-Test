using System.Windows;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow(RegistrationViewModel registrationViewModel)
        {
            InitializeComponent();
            DataContext = registrationViewModel;
        }
    }
}
