using System.Windows.Controls;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl.CustomControls
{
    /// <summary>
    /// Interaction logic for ViewControl.xaml
    /// </summary>
    public partial class InformationControl : UserControl
    {
        public InformationControl(InformationViewModel informationViewModel)
        {
            InitializeComponent();
            DataContext = informationViewModel;
        }
    }
}
