using Bogus;
using Bogus.DataSets;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;

namespace DevFreela.UnitTests.Mocks;

public static class ProjectMock
{
    public static Faker<Project> ProjectFaker => new Faker<Project>()
            .CustomInstantiator(f => (
                Project.From(
                    f.Random.Word(),
                    f.Random.Word(),
                    f.Random.Int(),
                    f.Random.Int(),
                    f.Random.Decimal()
                )
            ));

    public static Faker<CreateProjectCommand> CreateProjectCommandFaker =>
            new Faker<CreateProjectCommand>()
                .RuleFor(x => x.Title, f => f.Random.Word())
                .RuleFor(x => x.Description, f => f.Random.Word())
                .RuleFor(x => x.IdClient, f => f.Random.Int())
                .RuleFor(x => x.IdFreelancer, f => f.Random.Int())
                .RuleFor(x => x.TotalCost, f => f.Random.Decimal());

    public static Faker<PaginationResult<Project>> PaginationResultFaker => new Faker<PaginationResult<Project>>()
            .CustomInstantiator(f => new PaginationResult<Project>(
                    f.Random.Int(),
                    f.Random.Int(),
                    f.Random.Int(),
                    f.Random.Int(),
                    ProjectFaker.Generate(2)
                )
            );
}
