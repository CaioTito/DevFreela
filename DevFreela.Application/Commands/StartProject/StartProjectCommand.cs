using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommand : IRequest<Unit>
{
    public int Id { get; private set; }

    public StartProjectCommand(int id)
    {
        Id = id;
    }
}
