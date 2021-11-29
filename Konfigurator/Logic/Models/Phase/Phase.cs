namespace Konfigurator.Logic.Models.Phase
{
    public class Phase
    {
        public Phase(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}