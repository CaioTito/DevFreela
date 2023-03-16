using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<List<ProjectViewModel>>
{
    public string query { get; private set; }

    public GetAllProjectsQuery(string query)
    {
        this.query = query;
    }
}
