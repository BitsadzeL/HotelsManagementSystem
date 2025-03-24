using Hotels.Models.Dtos.Rooms;
using Hotels.Models.Entities;

namespace Hotels.Service.Interfaces
{
    public interface IRoomService
    {
        Task<RoomGettingDto> GetSingleRoom(int id);
        Task<List<RoomGettingDto>> GetAllRooms();
        Task<List<RoomGettingDto>> GetAllRoomsOfHotel(int hotelId);
        Task<List<RoomGettingDto>> FilterRooms(int? hotelid, string? isavailable, float? minprice, float? maxprice);
        Task AddNewRoom(RoomAddingDto roomAddingDto);
        Task UpdateRoom(RoomUpdatingDto roomUpdatingDto);
        Task ChangeStatus(int roomId);
        Task DeleteRoom(int id);
        Task DeleteRoomWithHotel(int id);
        Task SaveRoom();
    }
}
