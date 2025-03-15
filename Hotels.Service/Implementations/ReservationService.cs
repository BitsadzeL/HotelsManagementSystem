using AutoMapper;
using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Entities;
using Hotels.Repository.Implementations;
using Hotels.Repository.Interfaces;
using Hotels.Service.Interfaces;

namespace Hotels.Service.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper=mapper;
        }
        public async Task<int> AddReservation(ReservationAddingDto reservationAddingDto)
        {
            var obj=_mapper.Map<Reservation>(reservationAddingDto);
            await _reservationRepository.AddAsync(obj);
            await _reservationRepository.Save();
            return obj.Id;
        }

        public async Task DeleteReservation(int reservationId)
        {
            var reservationToDelete= await _reservationRepository.GetAsync(r=>r.Id==reservationId);
            _reservationRepository.Remove(reservationToDelete);
        }

        public async Task<List<ReservationGettingDto>> GetAllReservations()
        {
            List<Reservation> reservations=await _reservationRepository.GetAllAsync();
            var res=_mapper.Map<List<ReservationGettingDto>>(reservations);
            return res;

        }

        public async Task<ReservationGettingDto> GetReservation(int reservationId)
        {
            Reservation reservation = await _reservationRepository.GetAsync(r=>r.Id==reservationId);

            var res = _mapper.Map<ReservationGettingDto>(reservation);
            return res;
        }

        public async Task<List<ReservationGettingDto>> GetReservationsOfRoom(int roomId)
        {
            List<Reservation> roomReservations = await _reservationRepository.GetAllAsync(r=>r.RoomId==roomId);

            var res=_mapper.Map<List<ReservationGettingDto>>(roomReservations);
            return res;
        }

        public async Task SaveReservation()
        {
            await _reservationRepository.Save();
        }

        public async Task UpdateReservation(ReservationUpdatingDto reservationUpdatingDto)
        {
            var reservationToUpdate = await _reservationRepository.GetAsync(r=>r.Id == reservationUpdatingDto.Id);

            _mapper.Map(reservationUpdatingDto, reservationToUpdate);

            await _reservationRepository.Update(reservationToUpdate);
        }
    }
}
