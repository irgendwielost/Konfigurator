using System;

namespace Konfigurator.Logic.Models.Order
{
    public class Order
    {
        public Order(int id, DateTime datum, bool neuOrBestand, int customerId, int housingId, int phaseId, double factorMult, double overallPrice)
        {
            Id = id;
            Datum = datum;
            NeuOrBestand = neuOrBestand;
            CustomerId = customerId;
            HousingId = housingId;
            PhaseId = phaseId;
            FactorMult = factorMult;
            OverallPrice = overallPrice;
        }

        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public bool NeuOrBestand { get; set; }
        public int CustomerId { get; set; }
        public int HousingId { get; set; }
        public int PhaseId { get; set; }
        public double FactorMult { get; set; }
        public double OverallPrice { get; set; }
    }
}