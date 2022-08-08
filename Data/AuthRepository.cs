using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mortgage_API.Model;

namespace Mortgage_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;
            
        }
        public bool IsUserExist(string userName)
        {
            var user = _context.User.Select(a=>a.UserName == userName);
            if(user != null){
                return false;
            }
            return true;
            
        }

        public ServiceResponse<string> Login(string userName, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var user = _context.User.FirstOrDefault(a=>a.UserName.ToLower().Equals(userName.ToLower()));
            if(user == null)
            {
                response.Success = false;
                response.Message = "User Details Not Found";                
            }
            else if(VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt))
            {
                response.Success = true;
                response.Data = user.UserName;
                response.Message = "Login Success";
            }
            else
            {
                response.Success = false;
                response.Message = "Password is Incorrect";
            }
            return response;

            
        }

        private static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public ServiceResponse<int> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if(IsUserExist(user.UserName)){
                response.Message = "User already Exists";
                response.Success = false;
                return response;
            }
            CreatePasswordHash(password,out byte[] passwordHash,out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.User.Add(user);
            _context.SaveChanges();
            response.Data = user.Id;
            return response;
            
        }

        private static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedHash.Length;i++)
                {
                    if(computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }                    
                }
                return true;

            }
        }

    }
}