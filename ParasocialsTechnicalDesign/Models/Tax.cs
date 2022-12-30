using Microsoft.EntityFrameworkCore;

namespace ParasocialsPOSAPI.Models
{
    public class Tax
    {
        public Guid TaxId { get; set; }
        public Guid GroupId {get; set;}
        public Group Group { get; set; }
        [Precision(14,2)]
        public decimal Amount { get; set; }
        public TaxType Type { get; set; }
        public TaxReason Reason { get; set; }
    }
}
