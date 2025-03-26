using Hotels.Models.Dtos.Manager;
using Hotels.Models.Dtos.Managers;

namespace Hotels.Service.Interfaces
{
    public interface IManagerService
    {
        Task<ManagerGettingDto> GetManager(int id);
        Task<ManagerGettingDto> GetManagerOfHotel(int hotelId);
        Task<List<ManagerGettingDto>> GetAllManagers();
        Task<List<string>> GetPhoneNumbersAsync();
        Task<List<string>> GetIdNumbersAsync();
        Task<List<int>> GetHotelsWithManagerAsync();

        Task AddNewManager(ManagerAddingDto managerAddingDto);
        Task UpdateManager(ManagerUpdatingDto managerUpdatingDto);
        Task DeleteManager(int id);
        Task DeleteManagerWithHotel(int id);
        Task SaveManager();

    }
}
