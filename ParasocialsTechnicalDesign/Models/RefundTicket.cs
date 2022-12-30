namespace ParasocialsPOSAPI.Models
{
    public class RefundTicket
    {
        public Guid RefundTicketId { get; set; }
        public Guid Order { get; set; }
        public DateTime RequestDate { get; set; }
        public Boolean Granted { get; set; }
        public String Reason { get; set; }
        public RefundTicketRefundType RefundType { get; set; }
    }
}
