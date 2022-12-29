namespace ParasocialsPOSAPI.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; }
        public List<Group> Groups { get; set; }
    }
}
