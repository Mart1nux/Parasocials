using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class TipDTO 
{
        public Guid TipId { get; set; }
        public String Giver { get; set; }
        public TipType Type { get; set; }
        public DateTime GivenDate { get; set; }
        public Employee Receiver { get; set; }
}