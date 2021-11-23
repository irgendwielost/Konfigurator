namespace Konfigurator.Models.Floor
{
    public class Floor
    {
        public Floor(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}