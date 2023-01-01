namespace ParasocialsPOSAPI.Data_Transfer_Objects
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public string MembershipCard { get; set; }

        //TODO:
        public string LoyaltyType { get; set; }
    }
}
