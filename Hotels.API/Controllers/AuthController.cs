using Hotels.Models.Dtos.Guests;
using Hotels.Models.Dtos.Identity;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IGuestService _guestService;

        public AuthController(IAuthService authService, IGuestService guestService)
        {
            _authService=authService;
            _guestService=guestService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterGuest([FromForm] GuestRegistrationDto guestRegistrationDto)

        {
            var result = await  _authService.RegisterGuest(guestRegistrationDto);

            GuestAddingDto guestAddingDto = new GuestAddingDto()
            { 
                Id=result,
                Name=guestRegistrationDto.Name,
                Surname=guestRegistrationDto.Surname,
                IdNumber=guestRegistrationDto.IdNumber,
                PhoneNumber=guestRegistrationDto.PhoneNumber,
            };

            await _guestService.AddGuest(guestAddingDto);
            await _guestService.SaveGuest();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, guestRegistrationDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);


        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, loginResponse, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
    }
}
