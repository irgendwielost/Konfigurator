using System;

namespace Konfigurator.Logic.Models.Order
{
    public class Order
    {
        public Order(int id, DateTime datum, bool neuOrBestand, double size, int customerId, int housingId, int phaseId, double factorMult, double overallPrice)
        {
            Id = id;
            Datum = datum;
            NeuOrBestand = neuOrBestand;
            Size = size;
            CustomerId = customerId;
            HousingId = housingId;
            PhaseId = phaseId;
            FactorMult = factorMult;
            OverallPrice = overallPrice;
        }

        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public bool NeuOrBestand { get; set; }
        public double Size { get; set; }
        public int CustomerId { get; set; }
        public int HousingId { get; set; }
        public int PhaseId { get; set; }
        public double FactorMult { get; set; }
        public double OverallPrice { get; set; }
    }
}