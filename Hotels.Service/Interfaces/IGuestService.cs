using Hotels.Models.Dtos.Guests;

namespace Hotels.Service.Interfaces
{
    public interface IGuestService
    {
        Task<List<GuestGettingDto>> GetAllGuests();
        Task<GuestGettingDto> GetGuest(int id);

        Task AddGuest(GuestAddingDto guestAddingDto);
        Task UpdateGuest(GuestUpdatingDto guestUpdatingDto);

        Task DeleteGuest(int id);
        Task SaveGuest();


    }
}
