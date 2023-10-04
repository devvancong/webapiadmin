using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace Webapi.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
       
    }
}
