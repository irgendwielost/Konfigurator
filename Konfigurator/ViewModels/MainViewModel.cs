using Konfigurator.UserControls;

namespace Konfigurator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new ArtikelViewModel();
        }
    }
}