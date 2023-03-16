using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserByIdQuery : IRequest<UserViewModel>
{
    public int Id { get; private set; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}
