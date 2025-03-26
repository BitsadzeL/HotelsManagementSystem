using Hotels.Models.Dtos.Guests;
using Hotels.Models.Dtos.Managers;

namespace Hotels.Service.Interfaces
{
    public interface IModifyUserService
    {
        Task DeleteGuest(int id);
        Task DeleteManager(int id);
        Task DeleteIdentityUser(int id);

        Task UpdateGuest(GuestUpdatingDto guestUpdatingDto);
        Task UpdateManager(ManagerUpdatingDto managerUpdatingDto);

        Task SaveGuest();
        Task SaveManager();
    }
}
