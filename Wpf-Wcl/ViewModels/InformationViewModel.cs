namespace Wpf_Wcl.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        private string _information;

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
