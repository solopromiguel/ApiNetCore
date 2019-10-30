using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication21.sakila;

namespace WebApplication21.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly new_schemaContext _context;

        public AuthRepository(new_schemaContext context)
        {
            _context = context;
        }
        public async Task<Users> Login(string username, string password)
        {
            var user = await _context.Users
               .FirstOrDefaultAsync(f => f.UserName.Equals(username.Trim(),
               StringComparison.InvariantCultureIgnoreCase));

            if (user == null)
                return null;
            

            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;

            return user;
        }   
        public async Task<Users> Register(Users user, string password)
        {

            using (var context=new new_schemaContext())
            {
                byte[] passwordHash, passwordSalt;

                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                //user.PasswordHash = passwordHash;
                //user.PasswordSalt = passwordSalt;
                await context.Users.AddAsync(user);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("************************************");
                    Debug.WriteLine(e.ToString());
                }
                return user;

            }
            
        }


        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(a => a.UserName.Equals(username.Trim(),
                 StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }

            return false;
        }
       
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // This not the only way to secure a password
            // HMAC -- Hash-based Message Authentication Code
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // We will use this key/salt to unlock the password hash
                passwordSalt = hmac.Key;
                // We need to provide our password as byte array by using Text.Encoding
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // This not the only way to secure a password
            // HMAC -- Hash-based Message Authentication Code
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }
        public async Task<Users> GetUser(string username)
        {
            try
            {
                return await _context.Users.Where(a => a.UserName.Equals(username.Trim(),
                  StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

           

        }

        public async Task<Users> ChangePassword(Users user, string password)
        {

            using (var context = new new_schemaContext())
            {
                Users userDto = await context.Users.FirstAsync(x=>x.Id==user.Id); 

                byte[] passwordHash, passwordSalt;

                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                //userDto.PasswordHash = passwordHash;
                //userDto.PasswordSalt = passwordSalt;

                await context.SaveChangesAsync();
               
                return userDto;

            }

        }




    }
}
