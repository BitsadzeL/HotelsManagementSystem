using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("api/hotel/booking")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IReservationService _reservationService;

        public BookingController(IBookingService bookingService, IReservationService reservationService)
        {
            _bookingService= bookingService;
            _reservationService = reservationService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var result= await _bookingService.GetAllBookings();
            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBookingsOfGuest(int userId)
        {
            var result = await _bookingService.GetBookingsOfUser(userId);
            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingWithReservationAddingDto bookingWithReservationDto)
        {
            await _bookingService.AddBooking(bookingWithReservationDto);
            await _bookingService.SaveBooking();

            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, bookingWithReservationDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBooking(id);
            await _reservationService.SaveReservation();
            await _bookingService.SaveBooking();

            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


    }
}
