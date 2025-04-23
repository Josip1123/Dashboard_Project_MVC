using System.Diagnostics;
using Business.Helpers;
using Business.Services;
using Dashboard_MVC.Views.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_MVC.Controllers;

public class DashboardController(ProjectService service) : Controller
{
    public async Task<IActionResult> Dashboard()
    {
        
        var entities = await service.GetAllAsync();
        var viewModel = new DashboardViewModel
        {
            CreateProject = new CreateProjectViewModel
            {
                Projects = entities
            }
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DashboardViewModel vm)
    {
        
        try
        {
            var entity = Factories.ProjectFactory.Create(vm);
            await service.CreateProjectAsync(entity);
            return RedirectToAction("Dashboard");
        }
        catch (Exception ex)
        {
            var entities = await service.GetAllAsync();
            Debug.WriteLine($"{ex}");
            vm.CreateProject.Projects = entities;
            ViewBag.ShowProjectForm = true;
            return View("Dashboard", vm);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await service.DeleteAsync(id);
        return RedirectToAction("Dashboard");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEdit(string id)
    {
        try
        {
            var entityToUpdate = await service.GetByIdAsync(id);
            
            var dashboardViewModel = new DashboardViewModel
            {
                CreateProject = new CreateProjectViewModel
                {
                    Projects = await service.GetAllAsync()
                },
                
                EditProject = new EditFormViewModel
                {
                    Id = entityToUpdate.Id,
                    ProjectName = entityToUpdate.ProjectName,
                    ClientName = entityToUpdate.ClientName,
                    Description = entityToUpdate.Description,
                    DateDue = entityToUpdate.DateDue,
                    Price = entityToUpdate.Price,
                    DateCreated = entityToUpdate.DateCreated,
                    IsCompleted = entityToUpdate.IsCompleted
                
                }
            };
            
            ViewBag.ShowEditForm = true;
            return View("Dashboard", dashboardViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> PostEdit(DashboardViewModel vm)
    {
            var entity = new ProjectEntity()
            {
                Id = vm.EditProject.Id,
                ProjectName = vm.EditProject.ProjectName,
                ClientName = vm.EditProject.ClientName,
                Description = vm.EditProject.Description,
                DateDue = vm.EditProject.DateDue,
                DateCreated = vm.EditProject.DateCreated,
                IsCompleted = vm.EditProject.IsCompleted,
                Price = vm.EditProject.Price
            };
            await service.UpdateAsync(entity);
            return RedirectToAction("Dashboard");
    }
    
}