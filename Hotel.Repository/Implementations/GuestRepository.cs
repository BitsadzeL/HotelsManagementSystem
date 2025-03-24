using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repository.Implementations
{
    public class GuestRepository : Repository<Guest>, IGuestRepository
    {
        private readonly ApplicationDbContext _context;

        public GuestRepository(ApplicationDbContext context) : base(context)
        {
            _context=context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guest entity)
        {
            var entityFromDb = await _context.Guests.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if(entityFromDb is not null)
            {
                entityFromDb.Name = entity.Name;
                entityFromDb.Surname = entity.Surname;
                entityFromDb.IdNumber = entity.IdNumber;
                entityFromDb.PhoneNumber = entity.PhoneNumber;
            }
        }


        public async Task<List<string>> GetGuestPhoneNumbersAsync()
        {
            return await _context.Guests.Select(g => g.PhoneNumber).ToListAsync();
        }

        public async Task<List<string>> GetGuestIdNumbersAsync()
        {
            return await _context.Guests.Select(g=>g.IdNumber).ToListAsync();
        }

    }
}
