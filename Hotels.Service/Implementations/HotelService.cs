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
        private readonly IRoomService _roomService;
        private readonly IManagerService _managerService;
        private readonly IRoomRepository _roomRepository;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper, IRoomService roomService, IManagerService managerService,IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _roomService=roomService;
            _managerService=managerService;
            _roomRepository = roomRepository;


        }

        public async Task AddNewHotel(HotelAddingDto model)
        {

            var obj=_mapper.Map<Hotel>(model);
            await _hotelRepository.AddAsync(obj);
        }

        public async Task DeleteHotel(int id)
        {
            var hotelToDelete= await _hotelRepository.GetAsync(x=>x.Id==id);


            var roomsOfHotel = await _roomRepository.GetAllAsync(x=>x.HotelId==id);
            foreach (var room in roomsOfHotel)
            {
                await _roomService.DeleteRoom(room.Id);
            }
            await _roomService.SaveRoom();
            _hotelRepository.Remove(hotelToDelete);


            var managerToDelete = await _managerService.GetManagerOfHotel(id);
            await _managerService.DeleteManager(managerToDelete.Id);
            await _managerService.SaveManager();

        }

        public async Task<List<HotelGettingDto>> FilterHotels(string country, string city, float? rating)
        {
            Expression<Func<Hotel, bool>> filter = h =>
                (string.IsNullOrEmpty(country) || h.Country == country) &&
                (string.IsNullOrEmpty(city) || h.City == city) &&
                (!rating.HasValue || h.Rating >= rating);

            List<Hotel> result = await _hotelRepository.GetAllAsync(filter);
            var obj = _mapper.Map<List<HotelGettingDto>>(result);
            return obj;
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
