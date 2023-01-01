using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class ShiftDTO
{
    
        public Guid ShiftId { get; set; }
        public Guid Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

}