using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the Compensation to the db Context. 
        /// </summary>
        /// <remarks>
        /// Decided to co-locate with the Employees rather than separate into 
        /// it's own separte context for simplicity and performance.
        /// </remarks>
        public DbSet<Compensation> Compensations { get; set; }
    }
}
