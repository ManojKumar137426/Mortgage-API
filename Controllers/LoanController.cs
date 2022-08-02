using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mortgage_API.Model;

namespace Mortgage_API.Controllers
{
    [ApiController]
    [Route("Loan")]
    public class LoanController: ControllerBase
    {
        private static Loan obj = new Loan();

        [HttpGet("Get")]
        public ActionResult<Loan> Get()
        {
            obj.FirstName = "Test 123";
            return Ok(obj);
        }
    }
}