using AutoMapper;
using Hotels.Models.Dtos.Rooms;
using Hotels.Models.Entities;
using Hotels.Repository.Interfaces;
using Hotels.Service.Interfaces;

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
            var obj=_mapper.Map<Room>(roomAddingDto);

            await _roomRepository.AddAsync(obj);
        }

        public async Task DeleteRoom(int id)
        {
            var roomToDelete = await _roomRepository.GetAsync(x => x.Id == id);
            _roomRepository.Remove(roomToDelete);
        }

        public async Task<List<RoomGettingDto>> GetAllRooms()
        {
            List<Room> rooms = await _roomRepository.GetAllAsync(includeProperties: "Reservations");
            var obj = _mapper.Map<List<RoomGettingDto>>(rooms);

            return obj;
        }

        public async Task<RoomGettingDto> GetSingleRoom(int id)
        {
            Room room = await _roomRepository.GetAsync(x=>x.Id==id,includeProperties: "Reservations");

            var obj=_mapper.Map<RoomGettingDto>(room);
            return obj;
        }

        public async Task SaveRoom()
        {
            await _roomRepository.Save();
        }

        public Task UpdateRoom(RoomUpdatingDto roomUpdatingDto)
        {
            throw new NotImplementedException();
        }


    }
   
}
