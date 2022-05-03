using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppAPI.Data;
using AppAPI.DTOs;
using AppAPI.Entities;
using AppAPI.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepo,IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers(){
            var user = await _userRepo.GetMembersAsync();
            return Ok(user);
        }
      
        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> GetUser(int id){
        //     return Ok(await _userRepo.GetuserByIdAsync(id));
        // }
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserByUsername(string username){
            var user = await _userRepo.GetMember(username);
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult> UpdatedUser(MemberUpdateDTO obj){
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepo.GetUserByUsernameAsync(userName);

            _mapper.Map(obj,user);

            _userRepo.Update(user);

            if(await _userRepo.SaveAsync()) return NoContent();

            return BadRequest("Failed to update Users");
        }
    }
}