using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mortgage_API.Dtos.Loan;
using Mortgage_API.Model;

namespace Mortgage_API
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Loan,GetLoanDTO>();
            CreateMap<Loan,AddLoanDTO>();
            CreateMap<AddLoanDTO,Loan>();
        }
        
    }
}