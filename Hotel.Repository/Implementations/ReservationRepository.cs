using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repository.Implementations
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
            _context=context;   
        }

        public async Task Save()
        {
           await  _context.SaveChangesAsync();
        }

        public async Task Update(Reservation entity)
        {
            var entityFromDb = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entityFromDb is not null)
            {
                entityFromDb.CheckIn = entity.CheckIn;
                entityFromDb.CheckOut = entity.CheckOut;
                entityFromDb.RoomId= entity.RoomId;
            }
        }
    }
}
