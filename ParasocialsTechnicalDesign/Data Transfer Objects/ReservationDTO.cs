using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class ReservationDTO
{
        public Guid ReservationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime Duration { get; set; }
        public string ReservationNotes { get; set; }
        public String Premise { get; set; }
}