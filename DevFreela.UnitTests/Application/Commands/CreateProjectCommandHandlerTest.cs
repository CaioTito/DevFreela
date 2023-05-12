using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Mocks;
using Moq;

namespace DevFreela.UnitTests.Application.Commands;

public class CreateProjectCommandHandlerTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<ISkillRepository> _skillRepositoryMock;
    private readonly CreateProjectCommandHandler _createProjectCommandHandler;
    public CreateProjectCommandHandlerTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _skillRepositoryMock = new Mock<ISkillRepository>();
        _createProjectCommandHandler = new CreateProjectCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task InputDataIsOK_Executed_ReturnProjectId()
    {
        //Arrange
        var createProjectCommand = ProjectMock.CreateProjectCommandFaker.Generate();

        _unitOfWorkMock.SetupGet(uow => uow.Projects).Returns(_projectRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(uow => uow.Skills).Returns(_skillRepositoryMock.Object);

        //Act
        var id = await _createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

        //Assert
        Assert.True(id >= 0);

        _unitOfWorkMock.Verify(pr => pr.Projects.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}
