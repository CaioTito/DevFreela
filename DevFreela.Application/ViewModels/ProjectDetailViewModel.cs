namespace DevFreela.Application.ViewModels;

public class ProjectDetailViewModel
{
    public ProjectDetailViewModel(int myProperty, string title, string description, decimal totalCost, DateTime? startedAt, DateTime? finishedAt, string clientFullName, string freelancerFullName)
    {
        MyProperty = myProperty;
        Title = title;
        Description = description;
        TotalCost = totalCost;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
        ClientFullName = clientFullName;
        FreelancerFullName = freelancerFullName;        
    }

    public int MyProperty { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public string ClientFullName { get; private set; }
    public string FreelancerFullName { get; private set; }
}
