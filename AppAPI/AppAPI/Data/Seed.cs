using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context){
            if(await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach(var user in users){
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHarsh = hmac.ComputeHash(Encoding.UTF8.GetBytes("p@$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Add(user);     
            }

            await context.SaveChangesAsync();
        }
    }
}