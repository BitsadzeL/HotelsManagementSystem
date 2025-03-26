using Hotels.Models.Dtos.Hotel;

namespace Hotels.Service.Interfaces
{
    public interface IHotelService
    {
        //Task<List<TeacherForGettingDto>> GetAllTeachers();
        Task AddNewHotel(HotelAddingDto model);
        Task<List<HotelGettingDto>> GetAllHotels();
        Task<List<int>> GetHotelIds();
        Task<List<HotelGettingDto>> FilterHotels(string country, string city, float? rating);
        Task<HotelGettingDto> GetSingleHotel(int id);
        Task DeleteHotel(int id);

        Task UpdateHotel(HotelUpdatingDto model);
        Task SaveHotel();
    }
}
