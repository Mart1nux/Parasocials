using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class DiscountDTO 
{
        public Guid DiscountId { get; set; }
        public Group Group { get; set; }
        public DiscountType Type { get; set; }
        public decimal Ammount { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set;}
}