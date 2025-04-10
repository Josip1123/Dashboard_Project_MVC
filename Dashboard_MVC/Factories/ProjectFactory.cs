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
            ProjectName = vm.ProjectName,
            ClientName = vm.ClientName,
            Description = vm.Description,
            DateCreated = DateTime.Now,
            DateDue = vm.DateDue,
            Price = vm.Price ?? 0,
            
        };
    }
}