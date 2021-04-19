using System;
using System.Threading.Tasks;
using Application.Users.Commands.CreateRedBetUser;
using Application.Users.Commands.DeleteRedBetUser;
using Application.Users.Commands.UpdateRedBetUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("brand/RedBet/users")]
    public class RedBetUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RedBetUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateRedBetUser([FromBody] CreateRedBetUser request)
        {
            await _mediator.Send(request);
            return Created("user",null);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateRedBetUser([FromRoute] Guid id,[FromBody] UpdateRedBetUser request)
        {
            request.Id = id;
            await _mediator.Send(request);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteRedBetUser([FromRoute] Guid id,[FromBody] DeleteRedBetUser request)
        {
            request.Id = id;
            await _mediator.Send(request);
            return NoContent();
        }

    }
}