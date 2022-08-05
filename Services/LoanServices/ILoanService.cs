using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mortgage_API.Model;

namespace Mortgage_API.Services.LoanServices
{
    public interface ILoanService
    {
        ServiceResponse<List<Loan>> GetAllLoans();
        ServiceResponse<Loan> GetLoanById(int Id);

        ServiceResponse<List<Loan>> AddLoan(Loan objLoan);
        
    }
}