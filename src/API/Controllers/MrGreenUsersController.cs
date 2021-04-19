using System;
using System.Threading.Tasks;
using Application.Users.Commands.CreateMrGreenUser;
using Application.Users.Commands.DeleteMrGreenUser;
using Application.Users.Commands.UpdateMrGreenUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("brand/MrGreen/users")]
    public class MrGreenUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MrGreenUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateMrGreenUser([FromBody] CreateMrGreenUser request)
        {
            await _mediator.Send(request);
            return Created("user",null);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateMrGreenUser([FromRoute] Guid id,[FromBody] UpdateMrGreenUser request)
        {
            request.Id = id;
            await _mediator.Send(request);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteMrGreenUser([FromRoute] Guid id,[FromBody] DeleteMrGreenUser request)
        {
            request.Id = id;
            await _mediator.Send(request);
            return NoContent();
        }
    }
}