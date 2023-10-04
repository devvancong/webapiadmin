using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapi.Authenticate;
using WebDataModel.BaseClass;
using WebDataModel.ViewModel;
using WebService.Interface;

namespace Webapi.Controllers
{

    [Route("api/user")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticate _authenticate;

        public UsersController(IUserService userService, 
            IAuthenticate authenticate)
        {
            _userService = userService;
            _authenticate = authenticate;
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
        public async Task<IActionResult> Add([FromBody] Uservm uservm)
        {
            return Ok(await _userService.AddUser(uservm));
        }

        [HttpPut("edituser")]
        public async Task<IActionResult> Edit([FromBody] Uservm uservm)
        {
            return Ok(await _userService.UpdateUser(uservm));
        }

        [HttpDelete("deluser/{id}")]
        public async Task<IActionResult> Del(int id)
        {
            return Ok(await _userService.Delete(id));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin uservm)
        {
            return Ok(await _authenticate.AuthenticateUser(uservm));
        }
    }
}