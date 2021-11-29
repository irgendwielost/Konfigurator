namespace Konfigurator.Logic.Models.Package
{
    public class Package
    {
        public Package(int id, string name, bool available)
        {
            ID = id;
            Name = name;
            Available = available;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
    }
}