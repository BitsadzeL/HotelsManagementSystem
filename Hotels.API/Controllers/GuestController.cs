using Hotels.Models.Dtos.Guests;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("/api/hotel/guest")]
    public class GuestController : Controller       
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService= guestService;
        }
        [HttpPost]
        public async Task<IActionResult> AddGuest(GuestAddingDto guestAddingDto)
        {
            await _guestService.AddGuest(guestAddingDto);
            await _guestService.SaveGuest();
            return Ok(guestAddingDto);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllGuests()
        {
            var result = await _guestService.GetAllGuests();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getGuest([FromRoute] int id)
        {
            var result = await _guestService.GetGuest(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuest([FromBody] GuestUpdatingDto guestUpdatingDto)
        {
            await _guestService.UpdateGuest(guestUpdatingDto);
            await _guestService.SaveGuest();
            return Ok(guestUpdatingDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest([FromRoute] int id)
        {
            await _guestService.DeleteGuest(id);
            await _guestService.SaveGuest();
            return Ok();
        }
    }
}
