﻿using AutoMapper;
using Hotels.Models.Dtos.Hotel;
using Hotels.Models.Dtos.Rooms;
using Hotels.Models.Entities;
using Hotels.Repository.Implementations;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.Identity.Client;
using System.Linq.Expressions;
//saastumro,xelmisawvdomoba,fasisdiapazoni
namespace Hotels.Service.Implementations
{
    public class RoomService :IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository=roomRepository;
            _mapper=mapper;
        }


        public async Task AddNewRoom(RoomAddingDto roomAddingDto)
        {

            if (roomAddingDto.Price < 0)
            {
                throw new ArgumentException("Price must not be negative");
            }
            var obj=_mapper.Map<Room>(roomAddingDto);

            await _roomRepository.AddAsync(obj);
        }

        public async Task DeleteRoom(int id)
        {
            var roomToDelete = await _roomRepository.GetAsync(x => x.Id == id, includeProperties:"Reservations");

            if(roomToDelete is null)
            {
                throw new NotFoundException($"Room not found with id {id}");
            }

            if(roomToDelete.Reservations.Any())
            {
                throw new DeletionNotAllowedException("Room has active reservations. Unable to delete");
            }
            _roomRepository.Remove(roomToDelete);
        }

        public async Task<List<RoomGettingDto>> GetAllRooms()
        {
            List<Room> rooms = await _roomRepository.GetAllAsync(includeProperties: "Reservations");
            var obj = _mapper.Map<List<RoomGettingDto>>(rooms);

            return obj;
        }

        public async Task<List<RoomGettingDto>> GetAllRoomsOfHotel(int hotelId)
        {
            List<Room> rooms = await _roomRepository.GetAllAsync(x=>x.HotelId==hotelId,includeProperties: "Reservations");
            var obj = _mapper.Map<List<RoomGettingDto>>(rooms);

            return obj;
        }

        public async Task<RoomGettingDto> GetSingleRoom(int id)
        {
            Room room = await _roomRepository.GetAsync(x=>x.Id==id,includeProperties: "Reservations");

            var obj=_mapper.Map<RoomGettingDto>(room);
            return obj;
        }


        public async Task<List<RoomGettingDto>> FilterRooms(int? hotelid, string? isavailable, float? minprice, float? maxprice)
        {
            bool? isAvailableBool = null;
            if (!string.IsNullOrEmpty(isavailable) && bool.TryParse(isavailable, out bool parsedIsAvailable))
            {
                isAvailableBool = parsedIsAvailable;
            }

            Expression<Func<Room, bool>> filter = r =>
                (!isAvailableBool.HasValue || r.IsFree == isAvailableBool.Value) &&
                (!hotelid.HasValue || r.HotelId == hotelid) &&
                (!minprice.HasValue || r.Price >= minprice) &&
                (!maxprice.HasValue || r.Price <= maxprice);

            var result = await _roomRepository.GetAllAsync(filter);
            return _mapper.Map<List<RoomGettingDto>>(result);
        }


        public async Task SaveRoom()
        {
            await _roomRepository.Save();
        }

        public async Task UpdateRoom(RoomUpdatingDto roomUpdatingDto)
        {
            var roomToUpdate = await _roomRepository.GetAsync(x=>x.Id == roomUpdatingDto.Id);

            if(roomToUpdate is null)
            {
                throw new NotFoundException("Updating wrong room");
            }

            var obj = _mapper.Map(roomUpdatingDto, roomToUpdate);
            await _roomRepository.Update(obj);
        }

        public async Task ChangeStatus(int roomId)
        {
            var roomToChange = await _roomRepository.GetAsync(x=>x.Id == roomId);
            if(roomToChange is null)
            {
                throw new NotFoundException($"Room with id {roomId} not found");
            }

            roomToChange.IsFree = !roomToChange.IsFree;


        }

        public async Task DeleteRoomWithHotel(int id)
        {
            var roomToDelete = await _roomRepository.GetAsync(x=>x.Id == id);  
            _roomRepository.Remove(roomToDelete);
        }
    }
   
}
