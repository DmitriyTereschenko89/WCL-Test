namespace Wpf_Wcl.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        private string _information;

        public InformationViewModel(string information = "Default information.")
        {
            Information = information;
        }

        public string Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged(nameof(Information));
            }
        }
    }
}
