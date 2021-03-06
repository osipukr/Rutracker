﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rutracker.Server.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMapper _mapper;

        protected ApiController()
        {
        }

        protected ApiController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}