using Azure.Core;
using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
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
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddProjectCommentAsync(ProjectComment comment)
    {
        await _dbContext.ProjectComments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _dbContext.Projects.ToListAsync();
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

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task StartAsync(Project project)
    {
        //DAPPER
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            sqlConnection.Open();

            var script = "UPDATE Projects SET Status = @Status, StartedAt = @startedat WHERE Id = @id";

            await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
        }

        //EF
        //var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

        //project.Start();
        //_dbContext.SaveChanges();
    }
}
