using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Dtos.Rooms;
using Hotels.Service.Interfaces;
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


        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomAddingDto roomAddingDto)
        {
            await _roomService.AddNewRoom(roomAddingDto);
            await _roomService.SaveRoom();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, roomAddingDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            await _roomService.DeleteRoom(id);
            await _roomService.SaveRoom();


            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }








        [HttpPost("reservation")]
        public async Task<IActionResult> AddReservation([FromBody] ReservationAddingDto reservationAddingDto)
        {

            await _reservationService.AddReservation(reservationAddingDto);
            await _reservationService.SaveReservation();


            ApiResponse response = new(ApiResponseMessage.SuccessMessage, reservationAddingDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("reservation")]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationUpdatingDto reservationUpdatingDto)
        {
            await _reservationService.UpdateReservation(reservationUpdatingDto);
            await _reservationService.SaveReservation();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, reservationUpdatingDto, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{roomId}/reservation")]
        public async Task<IActionResult> getReservationsOfRoom([FromRoute] int roomId)
        {
            var result=await _reservationService.GetReservationsOfRoom(roomId);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("reservation")]
        public async Task<IActionResult> getAllReservations([FromRoute] int roomId)
        {
            var result = await _reservationService.GetAllReservations();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("reservation/{id}")]
        public async Task<IActionResult> getReservation([FromRoute] int id)
        {
            var result = await _reservationService.GetReservation(id);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("reservation/{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            await _reservationService.DeleteReservation(id);
            await _reservationService.SaveReservation();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


    }
}
