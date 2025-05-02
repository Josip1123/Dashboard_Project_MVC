using Data.Entities;

namespace Business.Interfaces;


    public interface IMemberService
    {
        Task CreateMemberAsync(MemberEntity project);
        Task<List<MemberEntity>> GetAllAsync();
        Task<MemberEntity> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync(MemberEntity memberEntity);
        
        Task<List<MemberEntity>> GetMembersById(List<string> ids);
    }
