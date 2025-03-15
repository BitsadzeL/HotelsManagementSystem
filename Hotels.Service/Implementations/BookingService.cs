using AutoMapper;
using Hotels.Models.Dtos.Bookings;
using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Entities;
using Hotels.Repository.Interfaces;
using Hotels.Service.Interfaces;

namespace Hotels.Service.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IReservationService reservationService)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _reservationService = reservationService;
        }
        public async Task AddBooking(BookingWithReservationAddingDto bookingWithReservationDto)
        {
            ReservationAddingDto reservationAddingDto = new();
            BookingAddingDto bookingAddingDto = new();


            //gamovyot Reservation
            reservationAddingDto.CheckIn = bookingWithReservationDto.CheckIn;
            reservationAddingDto.CheckOut = bookingWithReservationDto.CheckOut;
            reservationAddingDto.RoomId = bookingWithReservationDto.RoomId;
            int res = await _reservationService.AddReservation(reservationAddingDto);
            
             

            //gamovyot Booking
            bookingAddingDto.ReservationId = res;
            bookingAddingDto.GuestId= bookingWithReservationDto.GuestId;
            
            var booking = _mapper.Map<Booking>(bookingAddingDto);
            await _bookingRepository.AddAsync(booking);


        }

        public async Task DeleteBooking(int id)
        {
            var bookingToDelete = await _bookingRepository.GetAsync(x => x.Id == id);

            int reservationIdToDelete= bookingToDelete.ReservationId;
            await _reservationService.DeleteReservation(reservationIdToDelete);

            _bookingRepository.Remove(bookingToDelete);





        }

        public async Task<List<BookingGettingDto>> GetAllBookings()
        {
            List<Booking> bookings=await _bookingRepository.GetAllAsync();
            var obj=_mapper.Map<List<BookingGettingDto>>(bookings);
            return obj;
        }

        public Task<List<BookingGettingDto>> getBookingsOfRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookingGettingDto>> GetBookingsOfUser(int userId)
        {
            List<Booking> result = await _bookingRepository.GetAllAsync(x=>x.GuestId==userId);
            var obj = _mapper.Map<List<BookingGettingDto>>(result);
            return obj;
        }

        public Task<BookingGettingDto> GetSingleBooking(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveBooking()
        {
            await _bookingRepository.Save();
        }

        public Task UpdateBooking(BookingUpdatingDto bookingUpdatingDto)
        {
            throw new NotImplementedException();
        }
    }
}
