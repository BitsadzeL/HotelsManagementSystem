using Hotels.Models.Dtos.Hotel;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("api/hotel")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] HotelAddingDto model)
        {
            await _hotelService.AddNewHotel(model);
            await _hotelService.SaveHotel();

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var result=await _hotelService.GetAllHotels();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleHotel([FromRoute] int id)
        {
            var result=await _hotelService.GetSingleHotel(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        {
            await _hotelService.DeleteHotel(id);
            await _hotelService.SaveHotel();
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelUpdatingDto model)
        {
            await _hotelService.UpdateHotel(model);
            await _hotelService.SaveHotel();
            return Ok();
        }
    }
}
