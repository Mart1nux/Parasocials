namespace ParasocialsPOSAPI.Models
{
    public class Shift
    {
        public Guid Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Employee> Employees { get; set;}
    }
}
