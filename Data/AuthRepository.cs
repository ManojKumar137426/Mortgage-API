using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mortgage_API.Model;

namespace Mortgage_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext _context;
        public IConfiguration _config;
        public AuthRepository(DataContext context,IConfiguration config)
        {
            this._context = context;
            _config = config;
            
        }
        public async Task<bool> IsUserExist(string userName)
        {            
            if(await _context.User.AnyAsync(a=>a.UserName == userName))
            {                
                return true;
            }
            return false;
            
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
                response.Data = CreateToken(user);
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

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            try
            {
                if(await IsUserExist(user.UserName)){
                    response.Message = "User already Exists " + user.UserName;
                    response.Success = false;
                    return response;
                }
                CreatePasswordHash(password,out byte[] passwordHash,out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                response.Data = user.Id;
                response.Success = true;
                response.Message = "User Created Successfully";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                
            }
            
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

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}