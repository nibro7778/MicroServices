using Microsoft.AspNetCore.Mvc;
using MediatR;
using Branch.API.Features.BranchCRUD.Create;
using System.Threading.Tasks;
using Branch.API.Features.BranchCRUD.Get;
using Branch.API.Features.BranchCRUD.Delete;
using Branch.API.Features.BranchCRUD.Update;

namespace Branch.API.Features.BranchCRUD
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        public IMediator _mediator { get; set; }

        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBranchResponse>> Create([FromBody] CreateBranchRequest request)
        {
            var response = _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new { Id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<CreateBranchResponse>> Get([FromBody] GetBranchRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(DeleteBranchRequest request)
        {
            var response = _mediator.Send(request);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<UpdateBranchResponse>> Update(UpdateBranchRequest request)
        {
            var response = _mediator.Send(request);
            return Ok();
        }
    }
}