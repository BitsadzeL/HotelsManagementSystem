using Hotels.Models.Dtos.Rooms;
using Hotels.Models.Entities;

namespace Hotels.Service.Interfaces
{
    public interface IRoomService
    {
        Task<RoomGettingDto> GetSingleRoom(int id);
        Task<List<RoomGettingDto>> GetAllRooms();
        Task<List<RoomGettingDto>> GetAllRoomsOfHotel(int hotelId);
        Task AddNewRoom(RoomAddingDto roomAddingDto);
        Task UpdateRoom(RoomUpdatingDto roomUpdatingDto);
        Task DeleteRoom(int id);
        Task SaveRoom();
    }
}
