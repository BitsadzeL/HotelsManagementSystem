using Hotels.Models.Dtos.Guests;

namespace Hotels.Service.Interfaces
{
    public interface IGuestService
    {
        Task<List<GuestGettingDto>> GetAllGuests();
        Task<GuestGettingDto> GetGuest(int id);

        Task<List<string>> GetPhoneNumbersAsync();
        Task<List<string>> GetIdNumbersAsync();



        Task AddGuest(GuestAddingDto guestAddingDto);
        Task UpdateGuest(GuestUpdatingDto guestUpdatingDto);

        Task DeleteGuest(int id);
        Task SaveGuest();


    }
}
