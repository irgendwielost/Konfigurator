namespace Konfigurator.Logic.Models.Article
{
    public class Article
    {
        public Article(int id, string name, double price, bool available)
        {
            ID = id;
            Name = name;
            Price = price;
            Available = available;
        }


        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
    }
}