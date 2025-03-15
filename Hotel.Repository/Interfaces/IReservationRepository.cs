using Hotels.Models.Entities;

namespace Hotels.Repository.Interfaces
{
    public interface IReservationRepository:IRepository<Reservation>, IUpdateable<Reservation>, ISavable
    {
    }
}
