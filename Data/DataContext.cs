using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mortgage_API.Model;

namespace Mortgage_API.Data
{
    public class DataContext : DbContext
    {
        

        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Loan> Loans { get ; set; }

    }
}