using Hotels.Models.Dtos.Guests;
using Hotels.Models.Dtos.Identity;
using Hotels.Models.Dtos.Managers;
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
        private readonly IManagerService _managerService;

        public AuthController(IAuthService authService, IGuestService guestService, IManagerService managerService)
        {
            _authService=authService;
            _guestService=guestService;
            _managerService=managerService;
        }

        [HttpPost("registerguest")]
        public async Task<IActionResult> RegisterGuest([FromForm] GuestRegistrationDto guestRegistrationDto)
        {
            var result = await _authService.RegisterGuest(guestRegistrationDto);

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


        [HttpPost("registermanager")]
        public async Task<IActionResult> RegisterManager([FromForm] ManagerRegistrationDto managerRegistrationDto)
        {
            var result= await _authService.RegisterManager(managerRegistrationDto);

            ManagerAddingDto managerAddingDto = new ManagerAddingDto() 
            {
                Id=result,
                Name=managerRegistrationDto.Name,
                Surname = managerRegistrationDto.Surname,
                IdNumber=managerRegistrationDto.IdNumber, 
                Email=managerRegistrationDto.Email,
                PhoneNumber=managerRegistrationDto.PhoneNumber,
                HotelId= (int)(managerRegistrationDto.HotelId ?? null),

            };

            
            await _managerService.AddNewManager(managerAddingDto);
            await _managerService.SaveManager();
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, managerRegistrationDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] AdminRegistrationDto adminRegistrationDto)
        {
            await _authService.RegisterAdmin(adminRegistrationDto);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, adminRegistrationDto, 201, isSuccess: true);
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
