using Hotels.Models.Dtos.Identity;

namespace Hotels.Service.Interfaces
{
    public interface IAuthService
    {
        Task<int> RegisterGuest(GuestRegistrationDto guestRegistrationRequestDto);
        Task<int> RegisterManager(ManagerRegistrationDto managerRegistrationRequestDto);
        Task<int> RegisterAdmin(AdminRegistrationDto adminRegistrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
