using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result= await _bookingService.GetAllBookings();
            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("ofguest/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetBookingsOfGuest(int userId)
        {
            var result = await _bookingService.GetBookingsOfUser(userId);
            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("completed")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> GetCompletedReservations()
        {
            var result = await _reservationService.GetCompletedReservations();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("active")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> GetActiveReservations()
        {
            var result = await _reservationService.GetActiveReservations();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }



        //stumris reservationebi
        [HttpGet("reservations/ofguest/{guestid}")]
        [Authorize]
        public async Task<IActionResult> GetReservationsOfGuest([FromRoute] int guestId)
        {
            var result = await _bookingService.GetReservationsOfGuest(guestId);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("filterbydate")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> FilteByDate([FromQuery] DateTime? start,  [FromQuery] DateTime? end)
        {
            var result = await _reservationService.GetReservationsWithDate(start, end);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);

        }



        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddBooking(BookingWithReservationAddingDto bookingWithReservationDto)
        {
            await _bookingService.AddBooking(bookingWithReservationDto);
            await _bookingService.SaveBooking();

            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, bookingWithReservationDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        //[HttpDelete("{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteBooking(int id)
        //{
        //    await _bookingService.DeleteBooking(id);
        //    await _reservationService.SaveReservation();
        //    await _bookingService.SaveBooking();

            
        //    ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
        //    return StatusCode(response.StatusCode, response);
        //}


    }
}
