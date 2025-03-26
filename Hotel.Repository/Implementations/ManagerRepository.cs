using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repository.Implementations
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context) :base(context) 
        {
            _context = context;
            
        }

        public async Task<List<string>> GetManagerPhoneNumbersAsync()
        {
            return await _context.Managers.Select(g => g.PhoneNumber).ToListAsync();
        }

        public async Task<List<string>> GetManagerIdNumbersAsync()
        {
            return await _context.Managers.Select(g => g.IdNumber).ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Manager entity)
        {
            var entityFromDb = await _context.Managers.FirstOrDefaultAsync(x=>x.Id==entity.Id);

            if (entityFromDb is not null)
            {
                entityFromDb.Surname = entity.Surname;
                entityFromDb.Name = entity.Name;
                entityFromDb.IdNumber = entity.IdNumber;
                entityFromDb.PhoneNumber = entity.PhoneNumber;
                entityFromDb.Email = entity.Email;
                entityFromDb.HotelId = entity.HotelId;
            }
        }

        public async Task<List<int>> GetHotelsWithManagerAsync()
        {
            var result = await _context.Managers
                .Where(m => m.HotelId.HasValue)  
                .Select(m => m.HotelId.Value)    
                .ToListAsync();
            return result;
        }

    }
}
