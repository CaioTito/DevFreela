using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{
    public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommentCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _projectRepository.AddProjectCommentAsync(comment);

            return Unit.Value;
        }
    }
}
