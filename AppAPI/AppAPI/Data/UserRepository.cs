using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.DTOs;
using AppAPI.Entities;
using AppAPI.interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _map;
        public UserRepository(DataContext context, IMapper map)
        {
            _map = map;
            _context = context;
        }

        public async Task<MemberDTO> GetMember(string username)
        {
            return await _context.Users.Where(x=>x.UserName == username)
            .ProjectTo<MemberDTO>(_map.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        {
           return await _context.Users
            .ProjectTo<MemberDTO>(_map.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<AppUser> GetuserByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(x=>x.Photos).SingleOrDefaultAsync(x=>x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(x=>x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}