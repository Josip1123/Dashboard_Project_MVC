using System.Diagnostics;
using Business.Helpers;
using Business.Services;
using Dashboard_MVC.Views.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_MVC.Controllers;

public class DashboardController(ProjectService projectService, MemberService memberService) : Controller
{
    public async Task<IActionResult> Projects()
    {
        
        var entities = await projectService.GetAllAsync();
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
            await projectService.CreateProjectAsync(entity);
            return RedirectToAction("Projects");
        }
        catch (Exception ex)
        {
            var entities = await projectService.GetAllAsync();
            Debug.WriteLine($"{ex}");
            vm.CreateProject.Projects = entities;
            ViewBag.ShowProjectForm = true;
            return View("Projects", vm);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await projectService.DeleteAsync(id);
        return RedirectToAction("Projects");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEdit(string id)
    {
        try
        {
            var entityToUpdate = await projectService.GetByIdAsync(id);
            
            var dashboardViewModel = new DashboardViewModel
            {
                CreateProject = new CreateProjectViewModel
                {
                    Projects = await projectService.GetAllAsync()
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
            return View("Projects", dashboardViewModel);
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
            await projectService.UpdateAsync(entity);
            return RedirectToAction("Projects");
    }
    
    public async Task<IActionResult> Members()
    {
        
        var entities = await memberService.GetAllAsync();
        var viewModel = new DashboardViewModel
        {
            CreateMember = new CreateMemberViewModel
            {
                Members = entities
            }
        };

        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMember(DashboardViewModel vm)
    {
        
        try
        {
            var entity = Factories.MemberFactory.Create(vm);
            await memberService.CreateMemberAsync(entity);
            return RedirectToAction("Members");
        }
        catch (Exception ex)
        {
            var entities = await memberService.GetAllAsync();
            Debug.WriteLine($"{ex}");
            vm.CreateMember.Members = entities;
            ViewBag.ShowMemberForm = true;
            return View("Members", vm);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteMember(string id)
    {
        await memberService.DeleteAsync(id);
        return RedirectToAction("Members");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEditMember(string id)
    {
        try
        {
            var entityToUpdate = await memberService.GetByIdAsync(id);
            
            var dashboardViewModel = new DashboardViewModel
            {
                CreateMember = new CreateMemberViewModel()
                {
                    Members = await memberService.GetAllAsync()
                },
                
                EditMember = new EditMemberViewModel
                {
                    Id = entityToUpdate.Id,
                    FirstName = entityToUpdate.FirstName,
                    LastName = entityToUpdate.LastName,
                    Email  = entityToUpdate.Email,
                    Phone = entityToUpdate.Phone,
                    Title = entityToUpdate.Title
                }
            };
            
            ViewBag.ShowEditMemberForm = true;
            return View("Members", dashboardViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    [HttpPost]
    public async Task<IActionResult> PostEditMember(DashboardViewModel vm)
    {
        var entity = new MemberEntity()
        {
            Id = vm.EditMember.Id,
            FirstName = vm.EditMember.FirstName,
            LastName = vm.EditMember.LastName,
            Email = vm.EditMember.Email,
            Phone = vm.EditMember.Phone,
            Title = vm.EditMember.Title
           
        };
        await memberService.UpdateAsync(entity);
        return RedirectToAction("Members");
    }
}