namespace Konfigurator.Models.Article
{
    public class Article
    {
        public Article(int id, string name, double price, bool availabel)
        {
            ID = id;
            Name = name;
            Price = price;
            Availabel = availabel;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Availabel { get; set; }
    }
}