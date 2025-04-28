namespace Dashboard_MVC.Views.Models;

public class DashboardViewModel
{
    public CreateProjectViewModel CreateProject { get; set; } = null!;
    public EditFormViewModel EditProject { get; set; } = null!;
    
    public CreateMemberViewModel CreateMember { get; set; } = null!;
    
    public EditMemberViewModel EditMember { get; set; } = null!;
}