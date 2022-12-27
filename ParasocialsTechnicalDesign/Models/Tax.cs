namespace ParasocialsPOSAPI.Models
{
    public class Tax
    {
        public Guid Group { get; set; }
        public decimal Amount { get; set; }
        public TaxType Type { get; set; }
        public TaxReason Reason { get; set; }
    }
}
