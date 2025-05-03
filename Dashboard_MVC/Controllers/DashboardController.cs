using System.Diagnostics;
using Business.Services;
using Dashboard_MVC.Views.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_MVC.Controllers;

[Authorize]
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
            },
            EditProject = new EditFormViewModel()
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
            var allMembers = await memberService.GetAllAsync();
            
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
                    IsCompleted = entityToUpdate.IsCompleted,
                    Members = allMembers.ToList(),
                    AssignedMembers = entityToUpdate.Members.Select(m => m.Id).ToList()
                }
            };
            
            ViewBag.ShowEditForm = true;
            return View("Projects", dashboardViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Projects");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> PostEdit(DashboardViewModel vm)
    {
        
            var entity = await projectService.GetByIdAsync(vm.EditProject.Id);
            
               
                entity.ProjectName = vm.EditProject.ProjectName;
                entity.ClientName = vm.EditProject.ClientName;
                entity.Description = vm.EditProject.Description;
                entity.DateDue = vm.EditProject.DateDue;
                entity.IsCompleted = vm.EditProject.IsCompleted;
                entity.Price = vm.EditProject.Price;
                entity.Members.Clear();
            
            
            if (vm.EditProject.AssignedMembers != null && vm.EditProject.AssignedMembers.Any())
            {
                var selectedMemberEntities = await memberService.GetMembersById(vm.EditProject.AssignedMembers);
                foreach (var member in selectedMemberEntities)
                {
                    entity.Members.Add(member);
                }
            }
            
            await projectService.UpdateAsync(entity);
            return RedirectToAction("Projects");
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Members()
    {
        
        var entities = await memberService.GetAllAsync();
        var viewModel = new DashboardViewModel
        {
            CreateMember = new CreateMemberViewModel
            {
                Members = entities
            },
            EditMember = new EditMemberViewModel()
        };

        return View(viewModel);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateMember(DashboardViewModel vm)
    {
        
        try
        {
            if (vm.CreateMember.ImageFile != null && vm.CreateMember.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "members");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(vm.CreateMember.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.CreateMember.ImageFile.CopyToAsync(fileStream);
                }
                
                var entity = Factories.MemberFactory.Create(vm);
                entity.ImageUrl = "/uploads/members/" + fileName;
            
                await memberService.CreateMemberAsync(entity);
                return RedirectToAction("Members");
            }
            else
            {
                var entity = Factories.MemberFactory.Create(vm);
                await memberService.CreateMemberAsync(entity);
                return RedirectToAction("Members"); 
            }
            
            
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
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> DeleteMember(string id)
    {
        var member = await memberService.GetByIdAsync(id);
        
        if (!string.IsNullOrEmpty(member.ImageUrl) && !member.ImageUrl.Contains("user-placeholder"))
        {
            string fileName = Path.GetFileName(member.ImageUrl);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "members", fileName);
            
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        
        await memberService.DeleteAsync(id);
        
        return RedirectToAction("Members");
    }
    
    [Authorize(Roles = "Admin")]
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
                    Title = entityToUpdate.Title,
                    ImgUrl = entityToUpdate.ImageUrl
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
    
    [Authorize(Roles = "Admin")]
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
            Title = vm.EditMember.Title,
            ImageUrl = vm.EditMember.ImgUrl
           
        };
        
        if (vm.EditMember.ImageFile != null && vm.EditMember.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "members");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(vm.EditMember.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await vm.EditMember.ImageFile.CopyToAsync(fileStream);
            }
            entity.ImageUrl = "/uploads/members/" + fileName;
        }
        
        await memberService.UpdateAsync(entity);
        return RedirectToAction("Members");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> ChangeStatusCompleted(string id)
    {
        var entity = await projectService.GetByIdAsync(id);
        
        entity.IsCompleted = !entity.IsCompleted;

        await projectService.UpdateAsync(entity);
        
        return RedirectToAction("Projects");
    }
    
}