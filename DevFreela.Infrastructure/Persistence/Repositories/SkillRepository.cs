using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionString;
    public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }

    public async Task<List<SkillDto>> GetAllAsync()
    {
        //DAPPER
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            sqlConnection.Open();

            var script = "SELECT Id, Description FROM Skills";

            var skills = await sqlConnection.QueryAsync<SkillDto>(script);

            return skills.ToList();
        }

        //EF
        //var skills = _dbContext.Skills;

        //var skillsViewModel = skills
        //    .Select(s => new SkillViewModel(s.Id, s.Description))
        //    .ToList();

        //return skillsViewModel;
    }
}
