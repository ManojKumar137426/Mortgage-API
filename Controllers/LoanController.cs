using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mortgage_API.Model;
using Mortgage_API.Services.LoanServices;

namespace Mortgage_API.Controllers
{
    [ApiController]
    [Route("Loan")]
    public class LoanController: ControllerBase
    {
        private static Loan obj = new Loan();
        
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
            
        }


        [HttpGet("GetALL")]
        public ActionResult<List<Loan>> GetAll()
        {
            return Ok(_loanService.GetAllLoans());
        }
        [HttpGet("{id}")]
        public ActionResult<Loan> GetById(int id)
        {            
            return Ok(_loanService.GetLoanById(id));
        }

        [HttpPost]
        public ActionResult<List<Loan>> AddLoan(Loan obj)
        {
            return Ok(_loanService.AddLoan(obj));
        }
    }
}