namespace Konfigurator.Models.Room
{
    public class Room
    {
        public Room(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}