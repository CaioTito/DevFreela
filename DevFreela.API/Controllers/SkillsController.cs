using DevFreela.Application.Queries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllSkillsQuery();

        var skills = await _mediator.Send(query);

        return Ok(skills);
    }
}
