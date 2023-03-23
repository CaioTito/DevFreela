using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    public IActionResult GetById(int id)
    {
        var query = new GetUserByIdQuery(id);

        var user = _mediator.Send(query);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody] CreateUserCommand command)
    {
        var id = _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand login)
    {
        var loginUserViewModel = await _mediator.Send(login);

        if (loginUserViewModel == null)
        {
            return BadRequest();
        }

        return Ok(loginUserViewModel);
    }
}
