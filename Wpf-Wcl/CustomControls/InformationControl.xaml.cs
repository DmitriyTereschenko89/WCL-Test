using System.Windows.Controls;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl.CustomControls
{
    /// <summary>
    /// Interaction logic for ViewControl.xaml
    /// </summary>
    public partial class InformationControl : UserControl
    {
        public InformationControl(string information = "Default information")
        {
            InitializeComponent();
            var informationViewModel = new InformationViewModel();
            informationViewModel.Information = information;
            DataContext = informationViewModel;
        }
    }
}
