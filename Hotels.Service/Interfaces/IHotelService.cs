using Hotels.Models.Dtos.Hotel;

namespace Hotels.Service.Interfaces
{
    public interface IHotelService
    {
        //Task<List<TeacherForGettingDto>> GetAllTeachers();
        Task AddNewHotel(HotelAddingDto model);
        Task<List<HotelGettingDto>> GetAllHotels();
        Task<HotelGettingDto> GetSingleHotel(int id);
        Task DeleteHotel(int id);

        Task UpdateHotel(HotelUpdatingDto model);
        Task SaveHotel();
    }
}
