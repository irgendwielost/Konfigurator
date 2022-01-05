namespace Konfigurator.Logic.Models.Customer
{
    public class Customer
    {
        public Customer( int id, string name, int plz, string region, string street, string tel, string email, bool recent)
        {
            ID = id;
            Name = name;
            Plz = plz;
            Region = region;
            Street = street;
            Tel = tel;
            Email = email;
            Recent = recent;
        }

       


        public int ID { get; set; }
        public string Name { get; set; }
        public int Plz { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string Tel{ get; set; }
        public string Email{ get; set; }
        public bool Recent{ get; set; }
    }
}