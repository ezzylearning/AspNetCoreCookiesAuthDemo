using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCookiesAuthDemo.Models;

namespace AspNetCoreCookiesAuthDemo.Services
{
    public interface IAuthService
    {
        Task<User> ValidateUser(string username, string password);
    }
}
