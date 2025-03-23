using Hotels.Models.Dtos.Identity;

namespace Hotels.Service.Interfaces
{
    public interface IAuthService
    {
        Task<int> RegisterGuest(GuestRegistrationDto guestRegistrationRequestDto);
        Task<int> RegisterManager(ManagerRegistrationDto managerRegistrationRequestDto);
        Task RegisterAdmin(AdminRegistrationDto adminRegistrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task DeleteIdentityUser(int id);
    }
}
