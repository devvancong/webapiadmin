using Microsoft.AspNetCore.Mvc;
using WebDataModel.BaseClass;
using WebDataModel.ViewModel;
using WebService.Interface;

namespace Webapi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> Get([FromQuery] SearchParameters searchParameters)
        {
            return Ok(await _userService.GetAllUser(searchParameters));
        }

        [HttpGet("getbyuser/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.Get(id));
        }

        [HttpPost("adduser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromBody] Uservm uservm)
        {
            return Ok(await _userService.AddUser(uservm));
        }

        [HttpPut("edituser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Uservm uservm)
        {
            return Ok(await _userService.UpdateUser(uservm));
        }

        [HttpDelete("deluser/{id}")]
        public async Task<IActionResult> Del(int id)
        {
            return Ok(await _userService.Delete(id));
        }
    }
}