using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Rutracker.Server.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
    }
}