namespace Hotels.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut{ get; set; }



        public Room Room { get; set; }
        public int RoomId { get; set; }

        public List<Booking> Bookings { get; set; }

    }
}
