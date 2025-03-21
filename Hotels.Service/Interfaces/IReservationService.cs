using Hotels.Models.Dtos.Reservations;

//შესაძლებელია გაფილტვრა სასტუმროს, სტუმრის, ოთახის ან თარიღის მიხედვით.
namespace Hotels.Service.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationGettingDto> GetReservation(int reservationId);

        Task<List<ReservationGettingDto>> GetReservationsOfHotel(int hotelId);
        Task<List<ReservationGettingDto>> GetReservationsOfGuest(int guestId);
        Task<List<ReservationGettingDto>> GetReservationsOfRoom(int roomId);
        Task<List<ReservationGettingDto>> GetReservationsWithDate(DateTime? start, DateTime? end);

        Task<List<ReservationGettingDto>> GetActiveReservations();
        Task<List<ReservationGettingDto>> GetCompletedReservations();

        Task<List<ReservationGettingDto>> GetAllReservations();

        Task<int> AddReservation(ReservationAddingDto reservationAddingDto);
        Task UpdateReservation(ReservationUpdatingDto reservationUpdatingDto);
        Task DeleteReservation(int reservationId);

        Task SaveReservation();


    }
}
