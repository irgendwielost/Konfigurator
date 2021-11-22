namespace Konfigurator.Models.PackageDetails
{
    public class PackageDetails
    {
        public PackageDetails(int packageId, int articleId, int artMenge, double preis, bool recent)
        {
            Package_ID = packageId;
            Article_ID = articleId;
            ArtMenge = artMenge;
            Preis = preis;
            Recent = recent;
        }

        public int Package_ID { get; set; }
        public int Article_ID { get; set; }
        public int ArtMenge { get; set; }
        public double Preis { get; set; }
        public bool Recent { get; set; }
    }
}