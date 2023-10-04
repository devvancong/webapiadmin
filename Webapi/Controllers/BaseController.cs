using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace Webapi.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
       
    }
}
