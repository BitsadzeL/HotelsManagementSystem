using AutoMapper;
using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Entities;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.Identity.Client;

namespace Hotels.Service.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        //private readonly IBookingService _bookingService;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper=mapper;
            //_bookingService=bookingService;
        }
        public async Task<int> AddReservation(ReservationAddingDto reservationAddingDto)
        {
            var currentTime= DateTime.Now;
            if (reservationAddingDto.CheckIn < currentTime 
                || reservationAddingDto.CheckOut<currentTime 
                || reservationAddingDto.CheckOut<reservationAddingDto.CheckIn
                || reservationAddingDto.CheckIn == reservationAddingDto.CheckOut)
            {
                throw new InvalidDateException();
            }
            var obj=_mapper.Map<Reservation>(reservationAddingDto);
            await _reservationRepository.AddAsync(obj);
            await _reservationRepository.Save();
            return obj.Id;
        }

        public async Task DeleteReservation(int reservationId)
        {
            if (reservationId <= 0)
            {
                throw new ArgumentException($"Invalid argument passed {reservationId}");
            }


            var reservationToDelete= await _reservationRepository.GetAsync(r=>r.Id==reservationId);

            if (reservationToDelete is null)
            {
                throw new NotFoundException($"Reservation with id {reservationId} not found");
            }
            _reservationRepository.Remove(reservationToDelete);
        }

        public async Task<List<ReservationGettingDto>> GetActiveReservations()
        {
            var currentDate = DateTime.Now;
            var result = await _reservationRepository.GetAllAsync(r => r.CheckOut >= currentDate);

            var obj = _mapper.Map<List<ReservationGettingDto>>(result);
            return obj;
        }

        public async Task<List<ReservationGettingDto>> GetCompletedReservations()
        {
            var currentDate= DateTime.Now;
            var result = await _reservationRepository.GetAllAsync(r => r.CheckOut <= currentDate);

            var obj = _mapper.Map<List<ReservationGettingDto>>(result);
            return obj;
        }

        public async Task<List<ReservationGettingDto>> GetAllReservations()
        {
            List<Reservation> reservations=await _reservationRepository.GetAllAsync();

            if (!reservations.Any())
            {
                throw new NotFoundException("reservations not found");
            }

            var res=_mapper.Map<List<ReservationGettingDto>>(reservations);
            return res;

        }



        public async Task<ReservationGettingDto> GetReservation(int reservationId)
        {
            Reservation reservation = await _reservationRepository.GetAsync(r=>r.Id==reservationId);

            if (reservation is null)
            {
                throw new NotFoundException($"Reservation with {reservationId} not found");
            }

            var res = _mapper.Map<ReservationGettingDto>(reservation);
            return res;
        }

        public async Task<List<ReservationGettingDto>> GetReservationsOfGuest(int guestId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReservationGettingDto>> GetReservationsOfHotel(int hotelId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReservationGettingDto>> GetReservationsOfRoom(int roomId)
        {
            List<Reservation> roomReservations = await _reservationRepository.GetAllAsync(r=>r.RoomId==roomId);

            var res=_mapper.Map<List<ReservationGettingDto>>(roomReservations);
            return res;
        }

        public Task<List<ReservationGettingDto>> GetReservationsWithDate(DateTime? start, DateTime? end)
        {
            throw new NotImplementedException();
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
