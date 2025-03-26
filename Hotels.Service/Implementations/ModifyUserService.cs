using Hotels.Models.Dtos.Guests;
using Hotels.Models.Dtos.Managers;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;

namespace Hotels.Service.Implementations
{
    public class ModifyUserService : IModifyUserService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IGuestService _guestService;
        private readonly IManagerRepository _managerRepository;
        private readonly IManagerService _managerService;
        private readonly IAuthService _authService;

        public ModifyUserService(
            IGuestRepository guestRepository, 
            IGuestService guestService, 
            IManagerRepository managerRepository, 
            IManagerService managerService, 
            IAuthService authService)
        {
            _guestRepository = guestRepository;
            _guestService = guestService;
            _managerRepository = managerRepository;
            _managerService = managerService;
            _authService = authService;
        }
        public async Task DeleteGuest(int id)
        {
            await _guestService.DeleteGuest(id);


        }

        public async Task DeleteIdentityUser(int id)
        {
            await _authService.DeleteIdentityUser(id);
        }

        public async Task DeleteManager(int id)
        {
            await _managerService.DeleteManager(id);
        }

        public async Task SaveGuest()
        {
            await _guestService.SaveGuest();
        }

        public async Task SaveManager()
        {
            await _managerService.SaveManager();
        }

        public async Task UpdateGuest(GuestUpdatingDto guestUpdatingDto)
        {
            var userToUpdate = await _guestService.GetGuest(guestUpdatingDto.Id);
            if (userToUpdate is null)
            {
                throw new NotFoundException($"user with id {guestUpdatingDto.Id} was not found");
            }

            var guestidNumber = guestUpdatingDto.IdNumber;
            var guestPhoneNumber = guestUpdatingDto.PhoneNumber;

            var Ids = await _guestService.GetIdNumbersAsync();
            Ids.Remove(userToUpdate.IdNumber);

            var phoneNumbers = await _guestService.GetPhoneNumbersAsync();
            phoneNumbers.Remove(userToUpdate.PhoneNumber);

            if(phoneNumbers.Contains(guestPhoneNumber) || Ids.Contains(guestidNumber))
            {
                throw new DuplicateException("User with this phone number or ID number  already exists.");
            }

            await _guestService.UpdateGuest(guestUpdatingDto);


        }

        public Task UpdateManager(ManagerUpdatingDto managerUpdatingDto)
        {
            throw new NotImplementedException();
        }
    }
}
