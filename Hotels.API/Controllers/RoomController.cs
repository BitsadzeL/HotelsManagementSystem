using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Dtos.Rooms;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("api/hotel/room")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;

        public RoomController(IRoomService roomService, IReservationService reservationService)
        {
            _roomService = roomService;
            _reservationService = reservationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleRoom([FromRoute] int id)
        {
            var result = await _roomService.GetSingleRoom(id);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterRooms([FromQuery] int? hotelid, [FromQuery] string? isavailable, [FromQuery] float? minprice, [FromQuery] float? maxprice)
        {
            var result =  await _roomService.FilterRooms(hotelid, isavailable, minprice, maxprice);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);

        }


        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> AddRoom([FromBody] RoomAddingDto roomAddingDto)
        {
            await _roomService.AddNewRoom(roomAddingDto);
            await _roomService.SaveRoom();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, roomAddingDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            await _roomService.DeleteRoom(id);
            await _roomService.SaveRoom();


            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            await _roomService.ChangeStatus(id);
            await _roomService.SaveRoom();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);

        }


        [HttpPut]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomUpdatingDto roomUpdatingDto)
        {
            await _roomService.UpdateRoom(roomUpdatingDto);
            await _roomService.SaveRoom();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, roomUpdatingDto, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);

        }








        //[HttpPost("reservation")]
        //public async Task<IActionResult> AddReservation([FromBody] ReservationAddingDto reservationAddingDto)
        //{

        //    await _reservationService.AddReservation(reservationAddingDto);
        //    await _reservationService.SaveReservation();


        //    ApiResponse response = new(ApiResponseMessage.SuccessMessage, reservationAddingDto, 201, isSuccess: true);
        //    return StatusCode(response.StatusCode, response);
        //}

        [HttpPut("reservation")]
        [Authorize]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationUpdatingDto reservationUpdatingDto)
        {
            await _reservationService.UpdateReservation(reservationUpdatingDto);
            await _reservationService.SaveReservation();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, reservationUpdatingDto, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{roomId}/reservation")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> getReservationsOfRoom([FromRoute] int roomId)
        {
            var result=await _reservationService.GetReservationsOfRoom(roomId);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("reservation")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> getAllReservations()
        {
            var result = await _reservationService.GetAllReservations();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("reservation/{id}")]
        [Authorize]
        public async Task<IActionResult> getReservation([FromRoute] int id)
        {
            var result = await _reservationService.GetReservation(id);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("reservation/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            await _reservationService.DeleteReservation(id);
            await _reservationService.SaveReservation();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


    }
}
