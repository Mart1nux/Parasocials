namespace ParasocialsPOSAPI.Models
{
    public class Loyalty
    {
        public Customer Customer { get; set; }
        public double LoyaltyDiscount { get; set; }
        public LoyaltyType Type { get; set; }
    }
}
