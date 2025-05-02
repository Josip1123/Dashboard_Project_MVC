using Data.Entities;

namespace Business.Interfaces;

public interface IProjectService
{
    Task CreateProjectAsync(ProjectEntity project);
    Task<List<ProjectEntity>> GetAllAsync();
    Task<ProjectEntity> GetByIdAsync(string id);
    Task DeleteAsync(string id);
    Task UpdateAsync(ProjectEntity projectEntity);
}