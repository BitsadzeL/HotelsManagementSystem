using Hotels.Models.Dtos.Bookings;

namespace Hotels.Service.Interfaces
{
    public interface IBookingService
    {
        Task<BookingGettingDto> GetSingleBooking(int id);
        Task<List<BookingGettingDto>> GetAllBookings();

        Task<List<BookingGettingDto>> GetBookingsOfUser(int userId);
        Task<List<BookingGettingDto>> getBookingsOfRoom(int roomId);

        Task AddBooking(BookingWithReservationAddingDto bookingWithReservationDto);
        Task UpdateBooking(BookingUpdatingDto bookingUpdatingDto); 
        Task DeleteBooking(int id);
        Task SaveBooking();
    }
}
