using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Core.Entities.Identity;

namespace Rutracker.Server.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}