namespace Konfigurator.Models.Housing
{
    public class Housing
    {
        public Housing(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}