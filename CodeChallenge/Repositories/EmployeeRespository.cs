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
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            // TODO: Find better solution to shallow copy of the entity framework.
            // Using Include allows the EntityFramework to pull a deeper copy than the shallow copy it was pulling.
            // Side effect: All of the child objects from the first have an empty array instead of null, and levels
            // beyond the 3rd wouldn't be retrieved without further ThenIncludes or another solution. 
            // Not a performant solution on large data sets. This does allow me to continue to the rest of the implementation.
            // Original: return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);

            return _employeeContext.Employees.Include(m => m.DirectReports).ThenInclude(m => m.DirectReports).ThenInclude(m => m.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
