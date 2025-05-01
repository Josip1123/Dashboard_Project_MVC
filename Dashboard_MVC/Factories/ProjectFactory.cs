using Business.Helpers;
using Dashboard_MVC.Views.Models;
using Data.Entities;

namespace Dashboard_MVC.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(DashboardViewModel vm)
    {
        return new ProjectEntity()
        {
            Id = $"P - {IdGenerator.GenerateId(5)}",
            ProjectName = vm.CreateProject.ProjectName,
            ClientName = vm.CreateProject.ClientName,
            Description = vm.CreateProject.Description,
            DateCreated = DateTime.Now,
            DateDue = vm.CreateProject.DateDue,
            Price = vm.CreateProject.Price,
        };
    }
    
}