namespace Konfigurator.Logic
{
    public class Auftrag
    {
        public string ID { get; }
        public string KundeId { get; }

        public Auftrag(string Id, string kundeId)
        {
            ID = Id;
            KundeId = kundeId;
        }
    }
}