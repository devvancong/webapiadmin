using Microsoft.AspNetCore.Mvc;
using Webapi.Authenticate;
using WebDataModel.BaseClass;

namespace Webapi.Controllers
{
    [Route("api/token")]
    public class TokenController : BaseController
    {
        private readonly IAuthenticate _authenticate;

        public TokenController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
        {
            return Ok(await _authenticate.RefreshToken(tokenApiModel));
        }

        [HttpPost]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            return NoContent();
        }
    }
}