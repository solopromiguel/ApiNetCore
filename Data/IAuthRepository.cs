using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.sakila;

namespace WebApplication21.Data
{
    public interface IAuthRepository
    {
        Task<Users> Register(Users user, string password);

        Task<Users> Login(string username, string password);

        Task<bool> UserExists(string username);

        Task<Users> GetUser(string username);

        Task<Users> ChangePassword(Users user, string username);
    }
}
