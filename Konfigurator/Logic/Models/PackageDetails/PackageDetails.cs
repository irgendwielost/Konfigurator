namespace Konfigurator.Logic.Models.PackageDetails
{
    public class PackageDetails
    {
        public PackageDetails(int packageId, int articleId, int artMenge, double price, string articlename, bool recent)
        {
            Package_ID = packageId;
            Article_ID = articleId;
            ArtMenge = artMenge;
            Price = price;
            ArticleName = articlename;
            Recent = recent;
        }

        public int Package_ID { get; set; }
        public int Article_ID { get; set; }
        public int ArtMenge { get; set; }
        public double Price { get; set; }
        
        public string ArticleName { get; set; }
        public bool Recent { get; set; }
    }
}