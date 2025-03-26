using AutoMapper;
using Hotels.Models.Dtos.Guests;
using Hotels.Models.Entities;
using Hotels.Repository.Data;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.Identity.Client;

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

            if (string.IsNullOrWhiteSpace(guestAddingDto.IdNumber))
            {
                throw new ArgumentException("ID number can not be empty or null");
            }

            if (string.IsNullOrWhiteSpace(guestAddingDto.Name))
            {
                throw new ArgumentException("Name can not be empty or null");
            }

            if (string.IsNullOrWhiteSpace(guestAddingDto.Surname))
            {
                throw new ArgumentException("Surname can not be empty or null");
            }

            if (string.IsNullOrWhiteSpace(guestAddingDto.PhoneNumber))
            {
                throw new ArgumentException("Phone number can not be empty or null");
            }



            var obj=_mapper.Map<Guest>(guestAddingDto);
            await _guestRepository.AddAsync(obj);
        }

        public async Task DeleteGuest(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Guest id can not be negative or zero");
            }

            var guestToRemove=await _guestRepository.GetAsync(x=>x.Id==id, includeProperties:"Bookings");
            
            if (guestToRemove is null)
            {
                throw new NotFoundException($"Guest with id {id} not found");
            }


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
            if(guests is null)
            {
                throw new NotFoundException("there is no guest in database yet");
            }
            var res=_mapper.Map<List<GuestGettingDto>>(guests);
            return res;
        }

        public async Task<GuestGettingDto> GetGuest(int id)
        {

            if (id <= 0)
            {
                throw new ArgumentException("Guest id can not be negative or zero");
            }

            var result = await _guestRepository.GetAsync(c=>c.Id==id);
            if(result is null)
            {
                throw new NotFoundException($"Guest with id {id} not found");
            }
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
