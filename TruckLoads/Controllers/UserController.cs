using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckLoads.Services.MediatR.Commands.UserCommands;

namespace TruckLoads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async ValueTask<IActionResult> UserCreate([FromBody] UserCreateCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
