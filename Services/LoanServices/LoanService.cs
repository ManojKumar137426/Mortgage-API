using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mortgage_API.Data;
using Mortgage_API.Model;

namespace Mortgage_API.Services.LoanServices
{
    public class LoanService : ILoanService
    {
        private static List<Loan> loans = new List<Loan>{           
        };
        private readonly DataContext _dbContext;
        public LoanService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ServiceResponse<List<Loan>> AddLoan(Loan objLoan)
        {
            _dbContext.Add(objLoan);
            _dbContext.SaveChanges();
            var response = new ServiceResponse<List<Loan>>();
            response.Data = _dbContext.Loans.ToList();
            response.Message = "";
            response.Success = true;
            return response;
        }

        public ServiceResponse<List<Loan>> GetAllLoans()
        {
            var response = new ServiceResponse<List<Loan>>();
            response.Data = _dbContext.Loans.ToList();
            response.Message = "";
            response.Success = true;
            return response;
        }

        public ServiceResponse<Loan> GetLoanById(int Id)
        {
            var response = new ServiceResponse<Loan>();
            response.Data = loans.FirstOrDefault(a=>a.Id == Id);
            response.Message = "";
            response.Success = true;
            return response;
        }
    }
}