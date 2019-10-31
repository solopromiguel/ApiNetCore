using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Helpers;
using WebApplication21.sakila;

namespace WebApplication21.Data
{
   public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<Users>> GetUsers(UserParams userParams);
        Task<Users> GetUser(int id, bool isCurrentUser);
    }
}
