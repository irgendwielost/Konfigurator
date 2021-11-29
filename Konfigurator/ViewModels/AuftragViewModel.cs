using System.Collections.ObjectModel;
using System.Windows.Input;
using Konfigurator.Logic;

namespace Konfigurator.ViewModels
{
    public class AuftragViewModel : ViewModelBase
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private int _kundeId;

        public int kundeID
        {
            get
            {
                return _kundeId;
            }
            set
            {
                _kundeId = value;
                OnPropertyChanged(nameof(kundeID));
            }
        }

        private readonly ObservableCollection<Auftrag> _auftraege;

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public AuftragViewModel()
        {
            
        }
        
    }
}