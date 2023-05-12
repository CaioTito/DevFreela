namespace DevFreela.Core.Repositories;

public interface IUnitOfWork
{
    IProjectRepository Projects { get; }
    IUserRepository User { get; }
    Task<int> CompleteAsync();
}
