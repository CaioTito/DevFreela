using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface ISkillRepository
{
    Task<List<SkillDto>> GetAllAsync();

    Task AddSkillFromProject(Project project);
}
