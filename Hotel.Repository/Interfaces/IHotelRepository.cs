using Hotels.Models.Entities;

namespace Hotels.Repository.Interfaces
{
    public interface IHotelRepository : IRepository<Hotel>, ISavable,IUpdateable<Hotel>
    {

    }
}
