namespace Hotels.Models.Dtos.Reservations
{
    public class ReservationAddingDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
    }
}
