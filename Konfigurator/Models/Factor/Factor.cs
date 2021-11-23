namespace Konfigurator.Models.Factor
{
    public class Factor
    {
        public Factor(int id, string name, double mult, double grosse)
        {
            ID = id;
            Name = name;
            Mult = mult;
            Grosse = grosse;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public double Mult { get; set; }
        public double Grosse { get; set; }
    }
}