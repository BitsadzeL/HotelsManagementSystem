using Hotels.Models.Dtos.Bookings;
using Hotels.Models.Dtos.Reservations;

namespace Hotels.Service.Interfaces
{
    public interface IBookingService
    {
        Task<List<ReservationGettingDto>> GetReservationsOfGuest(int guestId);


        //
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
