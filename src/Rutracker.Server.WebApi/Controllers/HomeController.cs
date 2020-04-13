using Microsoft.AspNetCore.Mvc;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => RedirectPermanent("/swagger");
    }
}