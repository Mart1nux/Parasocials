using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class EmployeeDTO 
{
        public Guid Id { get; set;}
        public string Email { get; set; }
        public string password { get; set; }

        public decimal HourlyPayRate { get; set; }
        public Position Position { get; set; }
}