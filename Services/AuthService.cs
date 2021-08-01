using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCookiesAuthDemo.Data;
using AspNetCoreCookiesAuthDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCookiesAuthDemo.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
            
            return dbUser; 
        }
    }
}
