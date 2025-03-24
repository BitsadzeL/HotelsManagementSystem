using Hotels.Models.Entities;

namespace Hotels.Repository.Interfaces
{
    public interface IManagerRepository :IRepository<Manager>, IUpdateable<Manager>,ISavable
    {
        Task<List<string>> GetManagerPhoneNumbersAsync();
        Task<List<string>> GetManagerIdNumbersAsync();
    }
}
