using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.Data;
using AppAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string>GetScrete(){
            return "Secrete here";
        }
         [HttpGet("not-found")]
        public ActionResult<AppUser>getNotFound(){
           var item = _context.Users.Find(-1);
           if(item == null) return NotFound(); 
           return NotFound(item);
        }
         [HttpGet("server-error")]
        public ActionResult<string>GetServerError(){
            var item = _context.Users.Find(-1);
            var thingToReturn = item.ToString();
            return thingToReturn;
        }
         [HttpGet("bad-request")]
        public ActionResult<string>GetBadRequest(){
            return BadRequest("This was not a good request");
        }
    }
}