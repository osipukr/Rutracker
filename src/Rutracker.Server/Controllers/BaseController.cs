using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rutracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public class BaseController
    {
    }
}