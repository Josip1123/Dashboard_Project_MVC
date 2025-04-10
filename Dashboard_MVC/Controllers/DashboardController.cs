using System.Diagnostics;
using Business.Helpers;
using Business.Services;
using Dashboard_MVC.Views.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_MVC.Controllers;

public class DashboardController(ProjectService service) : Controller
{
    public async Task<IActionResult> Dashboard()
    {
        
        var entities = await service.GetAllAsync();
        var viewModel = new DashboardViewModel
        {
            Projects = entities,
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
            vm.Projects = entities;
            ViewBag.ShowProjectForm = true;
            return View("Dashboard", vm);
        }
    }
}