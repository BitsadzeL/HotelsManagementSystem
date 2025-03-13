using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repository.Implementations
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            _context=context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Room entity)
        {
            var entityFromDb=await _context.Rooms.FirstOrDefaultAsync(r => r.Id == entity.Id);

            if (entityFromDb is not  null)
            {
                entityFromDb.Title = entity.Title;
                entityFromDb.IsFree = entity.IsFree;
                entityFromDb.Price = entity.Price;
                entityFromDb.HotelId = entity.HotelId;
            }
        }
    }
}
