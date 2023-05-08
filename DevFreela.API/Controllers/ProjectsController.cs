using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Authorize(Roles ="client, freelancer")]
    public async Task<ActionResult> GetAll(string query)
    {
        var getAllProjectsQuery = new GetAllProjectsQuery(query);

        var projects = await _mediator.Send(getAllProjectsQuery);

        return Ok(projects);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> GetById(int id)
    {
        var getAllProjectsQuery = new GetProjectByIdQuery(id);

        var project = await _mediator.Send(getAllProjectsQuery);

        if (project == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
    {
        if (command.Description.Length > 200)
            return BadRequest();

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProjectCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{id}/comments")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> PostComment([FromBody] CreateProjectCommentCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/start")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Start(int id)
    {
        var command = new StartProjectCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/finish")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest("O pagamento não pôde ser processado.");
        }

        return Accepted();
    }
}
