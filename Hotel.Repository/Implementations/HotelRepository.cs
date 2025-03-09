using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repository.Implementations
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Hotel entity)
        {
            var entityFromDb = await _context.Hotels.FirstOrDefaultAsync(x=>x.Id==entity.Id);

            if (entityFromDb is not null)
            {
                entityFromDb.Title=entity.Title;
                entityFromDb.Address=entity.Address;
                entityFromDb.Rating=entity.Rating;
            }
        }
    }
}
