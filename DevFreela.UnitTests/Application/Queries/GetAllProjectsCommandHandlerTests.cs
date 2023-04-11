using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Mocks;
using Moq;

namespace DevFreela.UnitTests.Application.Queries;

public class GetAllProjectsCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly GetAllProjectsQuery _getAllProjectQuery;
    private readonly GetAllProjectsQueryHandler _getAllProjectQueryHandler;
    public GetAllProjectsCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _getAllProjectQuery = new GetAllProjectsQuery("");
        _getAllProjectQueryHandler = new GetAllProjectsQueryHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
    {
        //Arrange

        var projects = ProjectMock.ProjectFaker.Generate(3);

        _projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

        //Act
        var projectViewModelList = await _getAllProjectQueryHandler.Handle(_getAllProjectQuery, new CancellationToken());

        //Assert
        Assert.NotNull(projectViewModelList);
        Assert.NotEmpty(projectViewModelList);
        Assert.Equal(projects.Count, projectViewModelList.Count);

        _projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
    }
}
