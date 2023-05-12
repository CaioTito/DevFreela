namespace DevFreela.Core.Repositories;

public interface IUnitOfWork
{
    IProjectRepository Projects { get; }
    IUserRepository Users { get; }
    ISkillRepository Skills { get; }
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
}
