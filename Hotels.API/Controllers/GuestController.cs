﻿using Hotels.Models.Dtos.Guests;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("/api/hotel/guest")]
    public class GuestController : Controller       
    {
        private readonly IGuestService _guestService;
        private readonly IModifyUserService _modifyUserService;

        public GuestController(IGuestService guestService, IModifyUserService modifyUserService)
        {
            _guestService = guestService;
            _modifyUserService = modifyUserService;
        }



        //[HttpPost]
        //public async Task<IActionResult> AddGuest(GuestAddingDto guestAddingDto)
        //{
        //    await _guestService.AddGuest(guestAddingDto);
        //    await _guestService.SaveGuest();

        //    ApiResponse response = new(ApiResponseMessage.SuccessMessage, guestAddingDto, 201, isSuccess: true);
        //    return StatusCode(response.StatusCode, response);
        //}
        
        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> GetAllGuests()
        {
            var result = await _guestService.GetAllGuests();
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> getGuest([FromRoute] int id)
        {
            var result = await _guestService.GetGuest(id);
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);


        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateGuest([FromBody] GuestUpdatingDto guestUpdatingDto)
        {
            await _modifyUserService.UpdateGuest(guestUpdatingDto);
            await _guestService.SaveGuest();


            ApiResponse response = new(ApiResponseMessage.SuccessMessage, guestUpdatingDto, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGuest([FromRoute] int id)
        {
            await _modifyUserService.DeleteGuest(id);
            await _modifyUserService.DeleteIdentityUser(id);
            await _guestService.SaveGuest();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
    }
}
