using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Konfigurator.Logic.Models.Article;

namespace Konfigurator.ViewModels.Order
{
    public class AuftragViewModel : ViewModelBase
    {
        //ID
        //Aufträge
        //Artikel im Paket
        //Datum
        //Kunde ID
        //Gebäude Art
        //Phasen
        //Neu / Bestand
        
        //Order ID
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

        //Phase
        private string _phase;

        public string Phase
        {
            get
            {
                return _phase;
            }
            set
            {
                _phase = value;
                OnPropertyChanged(nameof(Phase));
            }
        }
        //Customer ID
        private int _kundeId;
        public int KundeId
        {
            get
            {
                return _kundeId;
            }
            set
            {
                _kundeId = value;
                OnPropertyChanged(nameof(KundeId));
            }
        }
        
        //Collection of Orders
        private readonly ObservableCollection<OrderViewmodel> _orders;
        public IEnumerable<OrderViewmodel> Orders => _orders;
        
        //Collection of Articles
        private readonly ObservableCollection<Article> _articles;
        

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public AuftragViewModel()
        {
            _orders = new ObservableCollection<OrderViewmodel>();
        }
        
    }
}