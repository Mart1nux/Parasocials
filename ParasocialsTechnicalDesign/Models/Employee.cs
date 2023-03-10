using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace ParasocialsPOSAPI.Models
{
    public class Employee
    {
        public Guid Id { get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        [Precision(14,2)]
        public decimal HourlyPayRate { get; set; }
        public Position Position { get; set; }
        public List<Shift> Shifts { get; set; }

        public List<Tip> Tips { get; set; }

    }
}
