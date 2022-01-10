using System;
using System.ComponentModel;

namespace Konfigurator.ViewModels.Order
{
    public class OrderViewmodel : ViewModelBase
    {
        private readonly Logic.Models.Order.Order _order;

        public int Id => _order.Id;
        public DateTime Datum => _order.Datum;
        public bool NeuOrBestand => _order.NeuOrBestand;
        public int CustomerId => _order.CustomerId;
        public int HousingId => _order.HousingId;
        public int PhaseId => _order.PhaseId;
        public double OverallPrice => _order.OverallPrice;


        public OrderViewmodel(Logic.Models.Order.Order order)
        {
            _order = order;
        }
    }
}