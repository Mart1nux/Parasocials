namespace ParasocialsPOSAPI.Models
{
    public class Tax
    {
        public Group Group { get; set; }
        public decimal Amount { get; set; }
        public TaxType Type { get; set; }
        public TaxReason Reason { get; set; }
    }
}
