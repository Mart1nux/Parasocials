using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParasocialsPOSAPI.Models
{
    public class Tip
    {
        public Guid TipId { get; set; }
        public String Giver { get; set; }
        public TipType Type { get; set; }
        public DateTime GivenDate { get; set; }
        public Employee Receiver { get; set; }
    }
}
