using DevFreela.Core.Enums;
using System.Xml.Linq;

namespace DevFreela.Core.Entities;

public class Project : BaseEntity
{
    public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;

        CreatedAt = DateTime.Now;
        Status = ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public int IdClient { get; private set; }
    public User Client { get; set; }
    public int IdFreelancer { get; private set; }
    public User Freelancer { get; set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public void Cancel() 
    {
        if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.Cancelled;
        }
        //TO DO - Criar Ex ceção no dominio para lançar nesse caso
    }
    public void Start()
    {
        if (Status == ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
        }
        //TO DO - Criar Ex ceção no dominio para lançar nesse caso
    }
    public void Finish()
    {
        if (Status == ProjectStatusEnum.PaymentPending)
        {
            Status = ProjectStatusEnum.Finished;
            FinishedAt = DateTime.Now;
        }
        //TO DO - Criar Ex ceção no dominio para lançar nesse caso
    }
    public void Update(string title, string description, decimal totalCost) 
    {
        Title = title; 
        Description = description;
        TotalCost = totalCost;
    }

    public void SetPaymentPending()
    {
        Status = ProjectStatusEnum.PaymentPending;
        FinishedAt = null;
    }

    public static Project From(string title, string description, int idClient, int idFreelancer, decimal totalCost)
    {
        return new Project(
            title,
            description,
            idClient,
            idFreelancer,
            totalCost
        );
    }
}
