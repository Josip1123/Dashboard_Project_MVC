using System.Diagnostics;
using Business.Interfaces;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class MemberService(MemberRepository repository) : IMemberService
{
    
        public async Task CreateMemberAsync(MemberEntity project)
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
    
        public async Task<List<MemberEntity>> GetAllAsync()
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
    
        public async Task<MemberEntity> GetByIdAsync(string id)
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
    
        public async Task UpdateAsync(MemberEntity memberEntity)
        {
           
            try
            {
                await repository.UpdateAsync(memberEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                throw;
            }
            
        }

        public async Task<List<MemberEntity>> GetMembersById(List<string> ids)
        {
            try
            {
                return await repository.GetMembersByIdsAsync(ids);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
}