using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AppAPI.Data;
using AppAPI.DTOs;
using AppAPI.Entities;
using AppAPI.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO regDto)
        {
            if(await UserExist(regDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = regDto.Username.ToLower(),
                PasswordHarsh = hmac.ComputeHash(Encoding.UTF8.GetBytes(regDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Add(user);

            await _context.SaveChangesAsync();

             return new UserDTO{
                  UserName = user.UserName,
                  Token = _tokenService.CreateToken(user)
              };
        }

          [HttpPost("login")]
          public async Task<ActionResult<UserDTO>> Login(LoginDTO logindto)
          {
              var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == logindto.Username);
              if(user == null) return Unauthorized("Invalid Username or password");
              using var hmac = new HMACSHA512(user.PasswordSalt);
              var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));

              for(int i = 0 ; i< computedHash.Length; i++){
                  if(computedHash[i] != user.PasswordHarsh[i]) return Unauthorized("Invalid username or password");
              }

              return new UserDTO{
                  UserName = user.UserName,
                  Token = _tokenService.CreateToken(user)
              };
          }
        private async Task<bool> UserExist(string username){
            return await _context.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }
}