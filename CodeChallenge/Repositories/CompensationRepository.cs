using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// This Compensation Repository helps persist and query the Compensation from the persistence layer.
    /// </summary>
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public CompensationRepository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        /// <summary>
        /// Persist the Compensation to datastore.
        /// </summary>
        /// <param name="compensation">The compensation object containing the salary, effectiveDate, and employeeId.</param>
        /// <returns>Returns the object back to the user.</returns>
        public Compensation Add(Compensation compensation)
        {
            _employeeContext.Compensations.Add(compensation);

            return compensation;
        }

        /// <summary>
        /// Allows querying Compensation from the datastore by id.
        /// </summary>
        /// <param name="id">The Employee Id.</param>
        /// <returns>Returns the selected compensation object to the user. Default if not found.</returns>
        public Compensation GetById(string id)
        {
            return _employeeContext.Compensations.SingleOrDefault(e => e.EmployeeId == id);
        }

        /// <summary>
        /// Saves all changes made to the conext to the datastore.
        /// </summary>
        /// <returns>Task with the result of the save action.</returns>
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

    }
}
