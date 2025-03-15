using Hotels.Models.Dtos.Bookings;

namespace Hotels.Models.Dtos.Guests
{
    public class GuestGettingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }

        public List<BookingGettingDto> Bookings { get; set; }
    }
}
