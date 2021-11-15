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
        
        
    }
}