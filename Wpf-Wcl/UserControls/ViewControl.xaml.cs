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

        public ViewControl(string information = "default information!")
        {
            InitializeComponent();
            Information = information;
        }

        public string Information
        {
            get => Dispatcher.Invoke(() => (string)GetValue(InformationProperty));
            set => Dispatcher.Invoke(() => SetValue(InformationProperty, value));
        }
    }
}
