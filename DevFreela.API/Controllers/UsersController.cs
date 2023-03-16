using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
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
    public IActionResult Post([FromBody] CreateUserCommand command)
    {        
        var id = _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    //[HttpPut("{id}/login")]
    //public IActionResult Login(int id, [FromBody] LoginModel login)
    //{
    //    TODO: Para Módulo de Autenticação e Autorização

    //    return NoContent();
    //}
}
