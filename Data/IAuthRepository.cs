using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mortgage_API.Model;

namespace Mortgage_API.Data
{
    public interface IAuthRepository
    {
        public ServiceResponse<int> Register(User user,string password);
        public ServiceResponse<string> Login(string userName,string password);
        public bool IsUserExist(string userName);
        
    }
}