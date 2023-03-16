using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment;

public class CreateProjectCommentCommand : IRequest<Unit>
{
    public string Content { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
}
