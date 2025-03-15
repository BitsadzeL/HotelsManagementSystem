using Hotels.Models.Dtos.Reservations;

namespace Hotels.Service.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationGettingDto> GetReservation(int reservationId);
        Task<List<ReservationGettingDto>> GetReservationsOfRoom(int roomId);
        Task<List<ReservationGettingDto>> GetAllReservations();

        Task<int> AddReservation(ReservationAddingDto reservationAddingDto);
        Task UpdateReservation(ReservationUpdatingDto reservationUpdatingDto);
        Task DeleteReservation(int reservationId);

        Task SaveReservation();


    }
}
