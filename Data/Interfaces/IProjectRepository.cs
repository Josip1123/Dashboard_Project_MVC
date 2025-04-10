using System.Linq.Expressions;
using Data.Entities;
namespace Data.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(ProjectEntity entity);
    Task<List<ProjectEntity>> GetAllAsync(); 
    Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task DeleteAsync(ProjectEntity entity);
    Task UpdateAsync(ProjectEntity entity);
}