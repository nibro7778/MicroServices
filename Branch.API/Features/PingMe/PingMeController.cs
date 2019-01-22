using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Branch.API.Features.PingMe
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingMeController : ControllerBase
    {
        [HttpGet("DoEcho")]
        public IActionResult DoEcho()
        {
            return Ok("Hello!!! DoEcho");
        }

        [HttpPost("DoEcho")]
        public IActionResult DoEcho(DoEchoRequest request)
        {
            return Ok($"Hello!!! {request.Value}");
        }
    }

    public class DoEchoRequest
    {
        public string Value { get; set; }
    }
}