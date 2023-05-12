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
        _getAllProjectQuery = new GetAllProjectsQuery{Query = "", Page = 1, PageSize = 1};
        _getAllProjectQueryHandler = new GetAllProjectsQueryHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
    {
        //Arrange

        var paginationProjects = ProjectMock.PaginationResultFaker.Generate();

        _projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()).Result).Returns(paginationProjects);

        //Act
        var paginationProjectViewModelList = await _getAllProjectQueryHandler.Handle(_getAllProjectQuery, new CancellationToken());

        //Assert
        Assert.NotNull(paginationProjectViewModelList);
        Assert.NotEmpty(paginationProjectViewModelList.Data);
        Assert.Equal(paginationProjects.Data.Count, paginationProjectViewModelList.Data.Count);

        _projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()).Result, Times.Once);
    }
}
