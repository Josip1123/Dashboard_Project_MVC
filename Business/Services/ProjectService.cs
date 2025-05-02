using System.Diagnostics;
using Business.Interfaces;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;



public class ProjectService(ProjectRepository repository) : IProjectService
{
    public async Task CreateProjectAsync(ProjectEntity project)
    {
        try
        {
            await repository.AddAsync(project);
            
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
           
            throw;
        }
    }

    public async Task<List<ProjectEntity>> GetAllAsync()
    {
        try
        {
            return await repository.GetAllAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
        
    }

    public async Task<ProjectEntity> GetByIdAsync(string id)
    {
        try
        {
            var project = await repository.GetAsync(x => x.Id == id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }
            return project;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }
    
  

    public async Task DeleteAsync(string id)
    {
      
        
        try
        {
            var entityToDelete = await GetByIdAsync(id);
            if (entityToDelete == null)
            {
                throw new Exception("Project not found");
            }

            
            await repository.DeleteAsync(entityToDelete);
            
           
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
          
            throw;
        }
        
    }

    public async Task UpdateAsync(ProjectEntity projectEntity)
    {
       
        try
        {
                await repository.UpdateAsync(projectEntity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            throw;
        }
        
    }
}