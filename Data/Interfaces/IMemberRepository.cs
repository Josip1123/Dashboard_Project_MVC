using System.Linq.Expressions;
using Data.Entities;

namespace Data.Interfaces;

public interface IMemberRepository
{
    Task AddAsync(MemberEntity entity);
    Task<List<MemberEntity>> GetAllAsync(); 
    Task<MemberEntity> GetAsync(Expression<Func<MemberEntity, bool>> expression);
    Task DeleteAsync(MemberEntity entity);
    Task UpdateAsync(MemberEntity entity);
}