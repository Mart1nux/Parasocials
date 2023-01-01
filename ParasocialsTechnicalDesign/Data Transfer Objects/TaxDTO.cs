using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class TaxDTO
{
        public Guid GroupId { get; set; }
        public decimal Amount { get; set; }
        public TaxType Type { get; set; }
        public TaxReason Reason { get; set; }
}