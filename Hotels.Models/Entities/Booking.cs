namespace Hotels.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
