﻿using AutoMapper;
using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Entities;
using Hotels.Repository.Implementations;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

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

        //public async Task<List<ReservationGettingDto>> GetReservationsOfGuest(int guestId)
        //{

        //    var reservations = await _reservationRepository.GetAllAsync( r => Id.Contains(r.Id));

        //    return _mapper.Map<List<ReservationGettingDto>>(reservations);

        //}

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

        public async Task<List<ReservationGettingDto>> GetReservationsWithDate(DateTime? start, DateTime? end)
        {
            Expression<Func<Reservation, bool>> filter = r =>
                (!start.HasValue || r.CheckIn >= start) &&
                (!end.HasValue || r.CheckOut <= end);

            var res = await _reservationRepository.GetAllAsync(filter);

            var obj = _mapper.Map<List<ReservationGettingDto>>(res);
            return obj;
        }


        public async Task SaveReservation()
        {
            await _reservationRepository.Save();
        }

        public async Task UpdateReservation(ReservationUpdatingDto reservationUpdatingDto)
        {
            bool isOverlapping = false;

            var currentTime = DateTime.Now;
            if (reservationUpdatingDto.CheckIn < currentTime
                || reservationUpdatingDto.CheckOut < currentTime
                || reservationUpdatingDto.CheckOut < reservationUpdatingDto.CheckIn
                || reservationUpdatingDto.CheckIn == reservationUpdatingDto.CheckOut)
            {
                throw new InvalidDateException();
            }


            var existingReservationsOfRoom = await _reservationRepository.GetAllAsync(x=>x.RoomId == reservationUpdatingDto.Id && x.Id != reservationUpdatingDto.Id);




            foreach (var r in existingReservationsOfRoom)
            {
                if ((reservationUpdatingDto.CheckIn < r.CheckOut && reservationUpdatingDto.CheckOut > r.CheckIn))
                {
                    isOverlapping = true;
                    throw new DateOverlapException($"Room {reservationUpdatingDto.RoomId} is not available from {reservationUpdatingDto.CheckIn} to {reservationUpdatingDto.CheckOut}.");
                }
            }

            var reservationToUpdate = await _reservationRepository.GetAsync(r=>r.Id == reservationUpdatingDto.Id);

            _mapper.Map(reservationUpdatingDto, reservationToUpdate);

            await _reservationRepository.Update(reservationToUpdate);
        }
    }
}
