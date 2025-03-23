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
            
            var user = await _context.Set<IdentityUser<int>>()
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            if (user == null)
            {
                throw new NotFoundException($"User {loginRequestDto.UserName} not found");
            }

            
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isValidPassword)
            {
                return new LoginResponseDto { Token = string.Empty }; 
            }

            
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


        public async Task RegisterAdmin(AdminRegistrationDto adminRegistrationRequestDto)
        {
            var user = _mapper.Map<IdentityUser<int>>(adminRegistrationRequestDto);


            var result = await _userManager.CreateAsync(user, adminRegistrationRequestDto.Password);

            if (result.Succeeded)
            {

                var userToReturn = await _userManager.FindByEmailAsync(adminRegistrationRequestDto.Email);

                if (userToReturn != null)
                {

                    if (!await _roleManager.RoleExistsAsync(_adminRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<int>(_adminRole));
                    }


                    await _userManager.AddToRoleAsync(userToReturn, _adminRole);


                    
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

        public async Task<int> RegisterManager(ManagerRegistrationDto managerRegistrationRequestDto)
        {
            var user = _mapper.Map<IdentityUser<int>>(managerRegistrationRequestDto);


            var result = await _userManager.CreateAsync(user, managerRegistrationRequestDto.Password);

            if (result.Succeeded)
            {

                var userToReturn = await _userManager.FindByEmailAsync(managerRegistrationRequestDto.Email);

                if (userToReturn != null)
                {

                    if (!await _roleManager.RoleExistsAsync(_managerRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<int>(_managerRole));
                    }


                    await _userManager.AddToRoleAsync(userToReturn, _managerRole);


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

        public async Task DeleteIdentityUser(int id)
        {
            
            var user = await _userManager.FindByIdAsync(id.ToString());

            
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }

            
            var result = await _userManager.DeleteAsync(user);

            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to delete user: {errors}");
            }
        }
    }
}
