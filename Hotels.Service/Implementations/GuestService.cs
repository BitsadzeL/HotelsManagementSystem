using AutoMapper;
using Hotels.Models.Dtos.Guests;
using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Hotels.Service.Interfaces;

namespace Hotels.Service.Implementations
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GuestService(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }
        public async Task AddGuest(GuestAddingDto guestAddingDto)
        {
            var obj=_mapper.Map<Guest>(guestAddingDto);
            await _guestRepository.AddAsync(obj);
        }

        public async Task DeleteGuest(int id)
        {
            var guestToRemove=await _guestRepository.GetAsync(x=>x.Id==id);
             _guestRepository.Remove(guestToRemove);
        }

        public async Task<List<GuestGettingDto>> GetAllGuests()
        {
            List<Guest> guests= await _guestRepository.GetAllAsync(includeProperties:"Bookings");
            var res=_mapper.Map<List<GuestGettingDto>>(guests);
            return res;
        }

        public async Task<GuestGettingDto> GetGuest(int id)
        {
            var result = await _guestRepository.GetAsync(c=>c.Id==id);
            var obj = _mapper.Map<GuestGettingDto>(result);
            return obj;
        }

        public async Task SaveGuest()
        {
            await _guestRepository.Save();
        }

        public async Task UpdateGuest(GuestUpdatingDto guestUpdatingDto)
        {
            var guestToUpdate = await _guestRepository.GetAsync(x=>x.Id==guestUpdatingDto.Id);
            var obj = _mapper.Map(guestUpdatingDto, guestToUpdate);
            await _guestRepository.Update(obj);
        }
    }
}
