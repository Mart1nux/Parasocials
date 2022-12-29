namespace ParasocialsPOSAPI.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public string MembershipCard { get; set; }


        public Guid LoyaltyId { get; set; }
        public Loyalty Loyalty {get; set; }
    }
}
