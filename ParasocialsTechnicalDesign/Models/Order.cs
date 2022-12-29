using System.Security.Cryptography.X509Certificates;

namespace ParasocialsPOSAPI.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public OrderState State { get; set; }
        public OrderDeliveryMethod DeliveryMethod { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderPaymentMethod PaymentMethod { get; set; }
        public string TransactionDetails { get; set; }
        public string TransactionCommnets { get; set; }
        public List<Product> Products { get; set; }
    }
}
