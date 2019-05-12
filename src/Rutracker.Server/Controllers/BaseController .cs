using Microsoft.AspNetCore.Mvc;

namespace Rutracker.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
    }
}