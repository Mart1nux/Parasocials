using System.ComponentModel.DataAnnotations;

namespace ParasocialsPOSAPI.Models
{
    public class Shift
    {
        public Guid ShiftId { get; set; }
        public Guid Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Employee> Employees { get; set;}
    }
}
