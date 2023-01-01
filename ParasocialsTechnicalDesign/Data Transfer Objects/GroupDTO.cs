using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

//NOTE: no such data in api
public class GroupDTO 
{
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Product> Products { get; set; }
}