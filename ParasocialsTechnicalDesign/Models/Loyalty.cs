namespace ParasocialsPOSAPI.Models
{
    public class Loyalty
    {
        public string CustomerId { get; set; }
        public double LoyaltyDiscount { get; set; }
        public LoyaltyType Type { get; set; }
    }
}
