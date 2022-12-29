namespace ParasocialsPOSAPI.Models
{
    public class Inventory
    {
        public Product product { get; set; }
        public int AmmountRemaining { get; set; }
        public DateTime LastRestockDate { get; set; }
    }
}
