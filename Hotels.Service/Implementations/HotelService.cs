using AutoMapper;
using Hotels.Models.Dtos.Hotel;
using Hotels.Models.Entities;
using Hotels.Repository.Interfaces;
using Hotels.Service.Interfaces;
using System.Linq.Expressions;

namespace Hotels.Service.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            
        }

        public async Task AddNewHotel(HotelAddingDto model)
        {

            var obj=_mapper.Map<Hotel>(model);
            await _hotelRepository.AddAsync(obj);
        }

        public async Task DeleteHotel(int id)
        {
            var hotelToDelete= await _hotelRepository.GetAsync(x=>x.Id==id);
            _hotelRepository.Remove(hotelToDelete);
        }

        public async Task<List<HotelGettingDto>> GetAllHotels()
        {
            List<Hotel> result = await _hotelRepository.GetAllAsync(includeProperties:"Manager,Rooms");
            var obj = _mapper.Map<List<HotelGettingDto>>(result);
            return obj;
        }

        public async Task<HotelGettingDto> GetSingleHotel(int id)
        {
            Hotel hotel= await _hotelRepository.GetAsync(x=>x.Id == id, includeProperties:"Manager");

            var result  = _mapper.Map<HotelGettingDto>(hotel);
            return result;
        }

        public async Task SaveHotel()
        {
            await _hotelRepository.Save();
        }

        public async Task UpdateHotel(HotelUpdatingDto model)
        {
            var HotelToUpdate = await _hotelRepository.GetAsync(x=>x.Id==model.Id);
            var obj=_mapper.Map(model, HotelToUpdate);
            await _hotelRepository.Update(obj);


        }
    }
}
