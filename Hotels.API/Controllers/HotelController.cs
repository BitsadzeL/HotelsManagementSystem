using Hotels.Models.Dtos.Guests;
using Hotels.Models.Dtos.Hotel;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddHotel([FromBody] HotelAddingDto model)
        {
            await _hotelService.AddNewHotel(model);
            await _hotelService.SaveHotel();
            
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, model, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var result=await _hotelService.GetAllHotels();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleHotel([FromRoute] int id)
        {
            var result=await _hotelService.GetSingleHotel(id);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        {
            await _hotelService.DeleteHotel(id);
            await _hotelService.SaveHotel();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        
        public async Task<IActionResult> UpdateHotel([FromBody] HotelUpdatingDto model)
        {
            await _hotelService.UpdateHotel(model);
            await _hotelService.SaveHotel();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, model, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterHotels(string? country, string? city, float? rating)
        {
            var result = await _hotelService.FilterHotels(country, city, rating);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
    }
}
