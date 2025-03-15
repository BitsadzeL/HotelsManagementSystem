public class BookingWithReservationAddingDto
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int RoomId { get; set; }

    public int GuestId { get; set; }
}