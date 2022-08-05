using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mortgage_API.Data;
using Mortgage_API.Dtos.Loan;
using Mortgage_API.Model;

namespace Mortgage_API.Services.LoanServices
{
    public class LoanService : ILoanService
    {
        private static List<Loan> loans = new List<Loan>{           
        };
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        public LoanService(IMapper mapper, DataContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public ServiceResponse<List<GetLoanDTO>> AddLoan(AddLoanDTO objLoan)
        {
            _dbContext.Add(_mapper.Map<Loan>(objLoan));
            _dbContext.SaveChanges();
            var response = new ServiceResponse<List<GetLoanDTO>>();
            response.Data = _dbContext.Loans.Select(loan=>_mapper.Map<GetLoanDTO>(loan)).ToList();
            response.Message = "";
            response.Success = true;
            return response;
        }

        public ServiceResponse<List<GetLoanDTO>> GetAllLoans()
        {
            var response = new ServiceResponse<List<GetLoanDTO>>();
            response.Data = _dbContext.Loans.Select(loan=>_mapper.Map<GetLoanDTO>(loan)).ToList();
            response.Message = "";
            response.Success = true;
            return response;
        }

        public ServiceResponse<GetLoanDTO> GetLoanById(int Id)
        {
            var response = new ServiceResponse<GetLoanDTO>();
            response.Data = _mapper.Map<GetLoanDTO>(_dbContext.Loans.FirstOrDefault(a=>a.Id == Id));
            response.Message = "";
            response.Success = true;
            return response;
        }

        public ServiceResponse<GetLoanDTO> UpdateLoan(UpdateLoanDTO objLoan)
        {
            var loan = _dbContext.Loans.FirstOrDefault(loan=>loan.Id == objLoan.Id);
            ServiceResponse<GetLoanDTO> response = new ServiceResponse<GetLoanDTO>();
            if(loan != null)
            {
                loan.FirstName = objLoan.FirstName;
                loan.LastName = objLoan.LastName;
                loan.LoanNumber = objLoan.LoanNumber;
                loan.LoanAmount = objLoan.LoanAmount;
                loan.LoanTerm = objLoan.LoanTerm;
                loan.LoanType = objLoan.LoanType;
                loan.Address = objLoan.Address;
            }
            _dbContext.SaveChanges();
            response.Data =  _mapper.Map<GetLoanDTO>(loan);
            response.Message = "";
            response.Success = true;
            return response;
        }
    }
}