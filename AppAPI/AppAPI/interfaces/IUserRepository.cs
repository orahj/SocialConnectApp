using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.DTOs;
using AppAPI.Entities;

namespace AppAPI.interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetuserByIdAsync(int Id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO> GetMember(string username);
    }
}