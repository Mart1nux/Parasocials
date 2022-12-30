namespace ParasocialsPOSAPI.Models
{
    public class Loyalty
    {
        public Guid LoyaltyId { get; set; }
        public Customer Customer { get; set; }
        public double LoyaltyDiscount { get; set; }
        public LoyaltyType Type { get; set; }
    }
}
