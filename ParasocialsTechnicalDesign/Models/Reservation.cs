namespace ParasocialsPOSAPI.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime Duration { get; set; }
        public string ReservationNotes { get; set; }
        public Premise Premise { get; set; }
    }
}
