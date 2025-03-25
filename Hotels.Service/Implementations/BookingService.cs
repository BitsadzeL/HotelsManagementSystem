using AutoMapper;
using Hotels.Models.Dtos.Bookings;
using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Entities;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;




namespace Hotels.Service.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IGuestService _guestService;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, 
            IMapper mapper, 
            IReservationService reservationService, 
            IRoomService roomService,
            IGuestService guestService)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _reservationService = reservationService;
            _roomService= roomService;
            _guestService = guestService;
        }
        public async Task AddBooking(BookingWithReservationAddingDto bookingWithReservationDto)
        {

            var roomToReserve = await _roomService.GetSingleRoom(bookingWithReservationDto.RoomId);
            if(roomToReserve.IsFree == false)
            {
                throw new RoomUnavailableException($"Room with id {bookingWithReservationDto.RoomId} is not available to reserve");
            }
            if(roomToReserve is null)
            {
                throw new NotFoundException("You are chosing wrong room.");
            }


            


            var existingReservationsOfRoom = await _reservationService.GetReservationsOfRoom(bookingWithReservationDto.RoomId);


            foreach (var r in existingReservationsOfRoom)
            {
                if ((bookingWithReservationDto.CheckIn >= r.CheckIn && bookingWithReservationDto.CheckIn < r.CheckOut) || 
                    (bookingWithReservationDto.CheckOut > r.CheckIn && bookingWithReservationDto.CheckOut <= r.CheckOut) || 
                    (bookingWithReservationDto.CheckIn <= r.CheckIn && bookingWithReservationDto.CheckOut >= r.CheckOut) || 
                    (bookingWithReservationDto.CheckIn > r.CheckOut && bookingWithReservationDto.CheckIn < r.CheckIn)) 
                {
                    throw new DateOverlapException($"Room {bookingWithReservationDto.RoomId} is not available from {bookingWithReservationDto.CheckIn} to {bookingWithReservationDto.CheckOut}.");
                }
            }


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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("id can not be negative or zero");
            }
            var bookingToDelete = await _bookingRepository.GetAsync(x => x.Id == id);
            if(bookingToDelete is null)
            {
                throw new NotFoundException($"Booking with {id} was not found");
            }

            int reservationIdToDelete= bookingToDelete.ReservationId;
            await _reservationService.DeleteReservation(reservationIdToDelete);

            _bookingRepository.Remove(bookingToDelete);

        }

        public async Task<List<BookingGettingDto>> GetAllBookings()
        {
            List<Booking> bookings=await _bookingRepository.GetAllAsync();
            if(bookings is null)
            {
                throw new NotFoundException("There are not any reservations in database");
            }
            var obj=_mapper.Map<List<BookingGettingDto>>(bookings);
            return obj;
        }

        public Task<List<BookingGettingDto>> getBookingsOfRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookingGettingDto>> GetBookingsOfUser(int userId)
        {
            var userToFind=await _guestService.GetGuest(userId);
            List<Booking> result = await _bookingRepository.GetAllAsync(x=>x.GuestId==userId);
            if(result is null || result.Count==0)
            {
                throw new NotFoundException("Bookings for this user was not found");
            }
            var obj = _mapper.Map<List<BookingGettingDto>>(result);
            return obj;
        }

        public async Task<List<ReservationGettingDto>> GetReservationsOfGuest(int guestId)
        {
            var userToFind = await _guestService.GetGuest(guestId);
            //bookingebidan avige is idebi, romelic am useris aris
            var reservationIds = await _bookingRepository.GetBookingIdsByGuestIdAsync(guestId);
            if(reservationIds is null  || reservationIds.Count == 0)
            {
                throw new NotFoundException("Reservations for this user was not found");
            }

            //avige is reservaciebi, romelta aidebic aris zeda listshi
            List<ReservationGettingDto> result = new();

            foreach (var reservation in reservationIds)
            {
                var reserve = await _reservationService.GetReservation(reservation);
                result.Add(reserve);
            }


            return result;


        }

        public Task<BookingGettingDto> GetSingleBooking(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveBooking()
        {
            await _bookingRepository.Save();
        }



        public async Task UpdateBooking(BookingUpdatingDto bookingUpdatingDto)
        {
            throw new NotImplementedException();
        }
    }
}
