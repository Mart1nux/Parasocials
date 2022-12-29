namespace ParasocialsPOSAPI.Models
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Product> Products { get; set; }
    }
}
