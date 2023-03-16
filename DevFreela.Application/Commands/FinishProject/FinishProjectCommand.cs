using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommand : IRequest<Unit>
{
    public int Id { get; private set; }

    public FinishProjectCommand(int id)
    {
        Id = id;
    }
}
