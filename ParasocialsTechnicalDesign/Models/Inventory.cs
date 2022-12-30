namespace ParasocialsPOSAPI.Models
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }
        public Product product { get; set; }
        public int AmmountRemaining { get; set; }
        public DateTime LastRestockDate { get; set; }
    }
}
