using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Branch.API.Features.PingMe.Post;

namespace Branch.API.Features.PingMe
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingMeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PingMeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("DoEcho")]
        public ActionResult DoEcho() {
            return Ok("Hello World!!!");
        } 

        [HttpPost("DoEcho")]
        public async Task<IActionResult> DoEcho(DoEchoRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }

    
}