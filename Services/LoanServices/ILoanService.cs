using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mortgage_API.Dtos.Loan;
using Mortgage_API.Model;

namespace Mortgage_API.Services.LoanServices
{
    public interface ILoanService
    {
        ServiceResponse<List<GetLoanDTO>> GetAllLoans();
        ServiceResponse<GetLoanDTO> GetLoanById(int Id);

        ServiceResponse<List<GetLoanDTO>> AddLoan(AddLoanDTO objLoan);

        ServiceResponse<GetLoanDTO> UpdateLoan(UpdateLoanDTO objLoan);
        
    }
}