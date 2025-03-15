using Hotels.Models.Entities;

namespace Hotels.Repository.Interfaces
{
    public interface IBookingRepository :IRepository<Booking>, IUpdateable<Booking>,ISavable
    {

    }
}
