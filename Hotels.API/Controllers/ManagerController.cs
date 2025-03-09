using Hotels.Models.Dtos.Managers;
using Hotels.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hotels.API.Controllers
{
    [ApiController]
    [Route("api/hotel")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet("managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            var result= await _managerService.GetAllManagers();
            return Ok(result);
        }

        [HttpGet("managers/{id}")]
        public async Task<IActionResult> GetAllManagers([FromRoute] int id)
        {
            var result = await _managerService.GetManager(id);
            return Ok(result);
        }

        [HttpPost("manager")]
        public async Task<IActionResult> AddNewManager([FromBody] ManagerAddingDto managerAddingDto)
        {
            await _managerService.AddNewManager(managerAddingDto);
            await _managerService.SaveManager();

            return Ok(managerAddingDto);
        }


        [HttpPut("manager")]
        public async Task<IActionResult> UpdateManager([FromBody] ManagerUpdatingDto managerUpdatingDto)
        {
            await _managerService.UpdateManager(managerUpdatingDto);
            await _managerService.SaveManager();

            return Ok(managerUpdatingDto);
        }



        [HttpDelete("manager/{id}")]
        public async Task<IActionResult> DeleteManager([FromRoute] int id)
        {
            await _managerService.DeleteManager(id);
            await _managerService.SaveManager();

            return NoContent();
        }
    }
}
