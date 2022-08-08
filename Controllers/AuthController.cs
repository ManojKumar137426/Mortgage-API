using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mortgage_API.Data;
using Mortgage_API.Dtos.User;
using Mortgage_API.Model;

namespace Mortgage_API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        public IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;            
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO request)
        {
            return Ok(await _authRepo.Register(new User{UserName=request.UserName},request.PassWord));
            
        }

        [HttpPost("Login")]
        public ActionResult<ServiceResponse<string>> Login(UserLoginDTO request)
        {
            var response = _authRepo.Login(request.UserName,request.PassWord);

            if(response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        
    }
}