using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.data;
using API.DTO;
using API.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AuthController: BaseController
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(RegisterDTO RegisterDTO){
            if( await IsExisted(RegisterDTO.Username)){
                return BadRequest("user existed");
            }
            using var hmac = new HMACSHA512(); 
            User obj = new User {
                Username = RegisterDTO.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(RegisterDTO.Password)),
                PasswordSalt = hmac.Key
                };
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return _context.Users.Find(obj.Id);
        }

         [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(LoginDTO LoginDTO){
            if (!await IsExisted(LoginDTO.Username))
            {
                return BadRequest("User not found");
            }
            User user =  _context.Users.First<User>(e => e.Username == LoginDTO.Username);
            var hmac = new HMACSHA512(user.PasswordSalt);
            if (user.PasswordHash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(LoginDTO.Password))))
            {
                return user;      
            }
            else {
                return BadRequest("Incorrect Password");
            }           
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Task<bool> IsExisted(string Username){
            return _context.Users.AnyAsync(e => e.Username == Username);
        }
    }
}