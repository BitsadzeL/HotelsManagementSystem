using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repository.Implementations
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<List<int>> GetBookingIdsByGuestIdAsync(int guestId)
        {
            return await _context.Bookings
                .Where(b => b.GuestId == guestId)
                .Select(b => b.Id)
                .ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Booking entity)
        {
            var entityFromDb = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entityFromDb is not null)
            {
                entityFromDb.ReservationId = entity.ReservationId;
                entityFromDb.GuestId = entity.GuestId;
            }
        }
    }
}
