using System.Windows;
using System.Windows.Controls;

namespace Wpf_Wcl.UserControls
{
    /// <summary>
    /// Interaction logic for ViewControl.xaml
    /// </summary>
    public partial class ViewControl : UserControl
    {
        public static readonly DependencyProperty InformationProperty = DependencyProperty.Register("Information", typeof(string), typeof(ViewControl));

        public ViewControl()
        {
            InitializeComponent();
            Information = "Please log in or sign up";
        }

        public string Information
        {
            get => (string)GetValue(InformationProperty);
            set => SetValue(InformationProperty, value);
        }
    }
}
