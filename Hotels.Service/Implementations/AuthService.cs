using AutoMapper;
using Hotels.Models.Dtos.Identity;
using Hotels.Repository.Data;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Service.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        private const string _adminRole = "Admin";
        private const string _customerRole = "Customer";
        private const string _managerRole = "Manager";


        public AuthService
            (ApplicationDbContext context,
            UserManager<IdentityUser<int>> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            // Find user by username (case-insensitive) - Query IdentityUser<int>
            var user = await _context.Set<IdentityUser<int>>()
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            if (user == null)
            {
                throw new NotFoundException($"User {loginRequestDto.UserName} not found");
            }

            // Check if password is valid
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isValidPassword)
            {
                return new LoginResponseDto { Token = string.Empty }; // Return empty token if password is incorrect
            }

            // Get user roles and generate JWT token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponseDto
            {
                Token = token
            };
        }




        public async Task<int> RegisterGuest(GuestRegistrationDto guestRegistrationRequestDto)
        {
            
            var user = _mapper.Map<IdentityUser<int>>(guestRegistrationRequestDto);

           
            var result = await _userManager.CreateAsync(user, guestRegistrationRequestDto.Password);

            if (result.Succeeded)
            {
                
                var userToReturn = await _userManager.FindByEmailAsync(guestRegistrationRequestDto.Email);

                if (userToReturn != null)
                {
                    
                    if (!await _roleManager.RoleExistsAsync(_customerRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<int>(_customerRole));
                    }

                    
                    await _userManager.AddToRoleAsync(userToReturn, _customerRole);

                    
                    return userToReturn.Id;
                }
                else
                {
                    
                    throw new InvalidOperationException("Failed to retrieve the user after registration.");
                }
            }
            else
            {
                
                throw new InvalidOperationException(result.Errors.FirstOrDefault()?.Description ?? "An error occurred while registering the user.");
            }
        }


        public Task<int> RegisterAdmin(AdminRegistrationDto adminRegistrationRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> RegisterManager(ManagerRegistrationDto managerRegistrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
