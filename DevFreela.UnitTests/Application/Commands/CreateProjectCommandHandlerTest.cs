using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Mocks;
using Moq;

namespace DevFreela.UnitTests.Application.Commands;

public class CreateProjectCommandHandlerTest
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly CreateProjectCommandHandler _createProjectCommandHandler;
    public CreateProjectCommandHandlerTest()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _createProjectCommandHandler = new CreateProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task InputDataIsOK_Executed_ReturnProjectId()
    {
        //Arrange
        var createProjectCommand = ProjectMock.CreateProjectCommandFaker.Generate();

        //Act
        var id = await _createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

        //Assert
        Assert.True(id >= 0);

        _projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}
