using System.Linq.Expressions;
using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context): BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public override async Task<List<ProjectEntity>> GetAllAsync()
    {
        
        return await _dbSet.Include(p => p.Members).ToListAsync();
    }
    
    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _dbSet.Include(p => p.Members).FirstOrDefaultAsync(expression);
    }


}
