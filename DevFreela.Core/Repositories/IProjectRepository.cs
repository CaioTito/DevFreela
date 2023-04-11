using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project> GetByIdAsync(int id);
    Task AddAsync(Project project);
    Task AddProjectCommentAsync(ProjectComment comment);
    Task SaveChangesAsync();
}
