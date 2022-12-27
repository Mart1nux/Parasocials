namespace ParasocialsPOSAPI.Models
{
    public class Employee
    {
        public string Email { get; set; }
        public string password { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public decimal HourlyPayRate { get; set; }
        public Guid Position { get; set; }
    }
}
