using Microsoft.EntityFrameworkCore;

namespace ParasocialsPOSAPI.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        [Precision(14,2)]
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; }
        public List<Group> Groups { get; set; }

        public Guid InventoryId {get; set;}
        public Inventory Inventory { get; set; }

        public Guid OrderId {get; set;}
        public Order Order { get; set; }
    }
}
