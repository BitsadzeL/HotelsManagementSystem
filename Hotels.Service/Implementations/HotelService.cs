using AutoMapper;
using Hotels.Models.Dtos.Hotel;
using Hotels.Models.Entities;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace Hotels.Service.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        private readonly IManagerService _managerService;
        //private readonly IRoomRepository _roomRepository;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper, IRoomService roomService, IManagerService managerService)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _roomService=roomService;
            _managerService=managerService;
            //_roomRepository = roomRepository;


        }

        public async Task AddNewHotel(HotelAddingDto model)
        {
            if(model.Rating <0 || model.Rating > 5)
            {
                throw new ArgumentException("Hotel rating must be in range 0-5");
            }

            var obj=_mapper.Map<Hotel>(model);
            await _hotelRepository.AddAsync(obj);
        }

        public async Task DeleteHotel(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Hotel id can not be negative or zero");
            }

            var hotelToDelete= await _hotelRepository.GetAsync(x=>x.Id==id);

            if(hotelToDelete is null)
            {
                throw new NotFoundException($"Hotel with id {id} was not found");
            }


            //sastumros otaxebis wamogheba da am otaxebis washla miuxedavad imisa aqvs tu ara javshani
            //var roomsOfHotel = await _roomRepository.GetAllAsync(x=>x.HotelId == id, includeProperties:"Reservations");
            var roomsOfHotel = await _roomService.GetAllRoomsOfHotel(id);
            if(roomsOfHotel is not null)
            {
                foreach (var room in roomsOfHotel)
                {
                    await _roomService.DeleteRoomWithHotel(room.Id);
                }
                await _roomService.SaveRoom();

            }

            


            //menejeris washla shezghudvebis gareshe
            var managerToDelete = await _managerService.GetManagerOfHotel(id);
            if(managerToDelete is not null)
            {
                await _managerService.DeleteManagerWithHotel(managerToDelete.Id);
                await _managerService.SaveManager();

            }

            _hotelRepository.Remove(hotelToDelete);

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

        public async Task<List<int>> GetHotelIds()
        {
            var result = await _hotelRepository.GetHotelIdsAsync();
            return result;
        }

        public async Task<HotelGettingDto> GetSingleHotel(int id)
        {
            Hotel hotel= await _hotelRepository.GetAsync(x=>x.Id == id, includeProperties:"Manager,Rooms");

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
