using Microsoft.EntityFrameworkCore;
namespace ParasocialsPOSAPI.Models
{
    public class Discount
    {
        public Guid DiscountId { get; set; }
        public Group Group { get; set; }
        public DiscountType Type { get; set; }
        [Precision(14,2)]
        public decimal Ammount { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set;}
    }
}
