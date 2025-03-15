namespace Hotels.Models.Dtos.Reservations
{
    public class ReservationUpdatingDto
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
    }
}
