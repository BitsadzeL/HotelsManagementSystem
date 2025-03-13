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

        public RoomController(IRoomService roomService)
        {
            _roomService=roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleRoom([FromRoute] int id)
        {
            var result = await _roomService.GetSingleRoom(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomAddingDto roomAddingDto)
        {
            await _roomService.AddNewRoom(roomAddingDto);
            await _roomService.SaveRoom();
            return Ok();
        }
    }
}
