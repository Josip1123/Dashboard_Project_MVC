using System.Diagnostics;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class ProjectService(ProjectRepository repository)
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

    /*public async Task UpdateAsync(string placeholder, string id)
    {
       
        try
        {
                var entityToUpdate = await GetByIdAsync(id);
                if (entityToUpdate == null)
                {
                    throw new Exception("Project not found");
                }
                entityToUpdate.Name = placeholder.Name;
                entityToUpdate.DateDue = placeholder.DateDue;
                entityToUpdate.IsCompleted = project.IsCompleted;#1#
                await repository.UpdateAsync(entityToUpdate);
               
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            throw;
        }
        
    }*/
}