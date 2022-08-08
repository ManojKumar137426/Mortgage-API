using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mortgage_API.Dtos.Loan;
using Mortgage_API.Model;
using Mortgage_API.Services.LoanServices;

namespace Mortgage_API.Controllers
{
    [ApiController]
    [Route("Loan")]
    
    public class LoanController: ControllerBase
    {        
        
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
            
        }


        [HttpGet("GetALL")]        
        public ActionResult<List<GetLoanDTO>> GetAll()
        {
            return Ok(_loanService.GetAllLoans());
        }
        [HttpGet("{id}")]
        public ActionResult<GetLoanDTO> GetById(int id)
        {            
            return Ok(_loanService.GetLoanById(id));
        }

        [HttpPost]
        public ActionResult<List<GetLoanDTO>> AddLoan(AddLoanDTO obj)
        {
            return Ok(_loanService.AddLoan(obj));
        }

        [HttpPut]
        public ActionResult<GetLoanDTO> UpdateLoan(UpdateLoanDTO obj)
        {
            return Ok(_loanService.UpdateLoan(obj));
        }

        [HttpDelete("{Id}")]
        public ActionResult<List<GetLoanDTO>> DeleteLoan(int Id)
        {
            return Ok(_loanService.DeleteLoan(Id));
        }
    }
}