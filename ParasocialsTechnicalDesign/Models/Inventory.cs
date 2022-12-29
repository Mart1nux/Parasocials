namespace ParasocialsPOSAPI.Models
{
    public class Inventory
    {
        public Guid productId { get; set; }
        public int AmmountRemaining { get; set; }
        public DateTime LastRestockDate { get; set; }
    }
}
