using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionString;

    public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }

    public async Task AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
    }

    public async Task AddProjectCommentAsync(ProjectComment comment)
    {
        await _dbContext.ProjectComments.AddAsync(comment);
    }

    public async Task<PaginationResult<Project>> GetAllAsync(string query, int page, int pageSize)
    {
        IQueryable<Project> projects = _dbContext.Projects;

        if (!string.IsNullOrWhiteSpace(query))
        {
            projects = projects
                .Where(p =>
                    p.Title.Contains(query) ||
                    p.Description.Contains(query));
        }
        return await projects.GetPaged<Project>(page, pageSize);
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        var project = await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == id);

        if (project == null)
            return null;

        return project;
    }

    //public async Task StartAsync(Project project)
    //{
    //    DAPPER
    //    using (var sqlConnection = new SqlConnection(_connectionString))
    //    {
    //        sqlConnection.Open();

    //        var script = "UPDATE Projects SET Status = @Status, StartedAt = @startedat WHERE Id = @id";

    //        await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
    //    }

    //    EF
    //    var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

    //    project.Start();
    //    _dbContext.SaveChanges();
    //}
}
