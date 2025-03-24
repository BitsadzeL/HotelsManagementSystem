using AutoMapper;
using Hotels.Models.Dtos.Guests;
using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;

namespace Hotels.Service.Implementations
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;
        //private readonly IAuthService _authService;

        public GuestService(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
            //_authService=authService;
        }
        public async Task AddGuest(GuestAddingDto guestAddingDto)
        {
            var obj=_mapper.Map<Guest>(guestAddingDto);



            await _guestRepository.AddAsync(obj);
        }

        public async Task DeleteGuest(int id)
        {
            var guestToRemove=await _guestRepository.GetAsync(x=>x.Id==id, includeProperties:"Bookings");

            if(guestToRemove.Bookings.Any())
            {
                throw new DeletionNotAllowedException("This guest can not be deleted, because of having active bookings");
            }
             _guestRepository.Remove(guestToRemove);
            //await _authService.DeleteIdentityUser(id);
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

        public async Task<List<string>> GetIdNumbersAsync()
        {
            var result = await _guestRepository.GetGuestIdNumbersAsync();
            return result;
        }

        public async Task<List<string>> GetPhoneNumbersAsync()
        {
            var result = await _guestRepository.GetGuestPhoneNumbersAsync();
            return result;
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
