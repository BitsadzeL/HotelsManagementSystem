using Hotels.Models.Entities;

namespace Hotels.Repository.Interfaces
{
    public interface IGuestRepository :IRepository<Guest>, IUpdateable<Guest>, ISavable
    {
        Task<List<string>> GetGuestPhoneNumbersAsync();
        Task<List<string>> GetGuestIdNumbersAsync();
    }
}
