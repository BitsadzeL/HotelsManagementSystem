using Hotels.Models.Entities;

namespace Hotels.Repository.Interfaces
{
    public interface IRoomRepository : IRepository<Room>, IUpdateable<Room>, ISavable
    {

    }
}
