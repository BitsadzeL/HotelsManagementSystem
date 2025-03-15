using Hotels.Models.Dtos.Managers;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("api/hotel/manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var result= await _managerService.GetAllManagers();

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllManagers([FromRoute] int id)
        {
            var result = await _managerService.GetManager(id);

            ApiResponse response = new(ApiResponseMessage.SuccessMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewManager([FromBody] ManagerAddingDto managerAddingDto)
        {
            await _managerService.AddNewManager(managerAddingDto);
            await _managerService.SaveManager();

            //post
            ApiResponse response = new(ApiResponseMessage.SuccessMessage, managerAddingDto, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateManager([FromBody] ManagerUpdatingDto managerUpdatingDto)
        {
            await _managerService.UpdateManager(managerUpdatingDto);
            await _managerService.SaveManager();


            ApiResponse response = new(ApiResponseMessage.SuccessMessage, managerUpdatingDto, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager([FromRoute] int id)
        {
            await _managerService.DeleteManager(id);
            await _managerService.SaveManager();


            ApiResponse response = new(ApiResponseMessage.SuccessMessage, id, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
    }
}
