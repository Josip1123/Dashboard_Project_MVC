using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class MemberRepository(DataContext context): BaseRepository<MemberEntity>(context), IMemberRepository
{
   public async Task<List<MemberEntity>> GetMembersByIdsAsync(List<string> ids)
   {
      
      if (ids == null || !ids.Any())
      {
         return new List<MemberEntity>();
      }
      
      return await _dbSet.Where(member => ids.Contains(member.Id)).ToListAsync();
   } 
}