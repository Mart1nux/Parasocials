namespace ParasocialsPOSAPI.Models
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Product> Products { get; set; }
        
        public Guid DiscountId {get; set;}
        public Discount Discount {get; set; }
        public Tax Tax {get; set; }
    }
}
