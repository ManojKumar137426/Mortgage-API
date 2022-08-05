using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mortgage_API.Dtos.Loan
{
    public class GetLoanDTO
    {
        public int Id{get;set;}
        public string FirstName {get;set;} = "Manoj Kumar";
        public string LastName {get;set;} = "Bejjarapu";
        public string LoanNumber{get;set;}
        public double LoanAmount{get;set;}
        public string LoanType{get;set;}
        public int LoanTerm{get;set;}
        public string Address{get;set;}
    }
}