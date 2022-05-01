using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.Entities;

namespace AppAPI.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}