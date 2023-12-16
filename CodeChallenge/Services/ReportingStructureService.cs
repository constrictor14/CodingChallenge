using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CodeChallenge.Services
{
    /// <summary>
    /// The Reporting Structure Service provides a separation of business logic from the associated controller.
    /// </summary>
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// This Gets the Reporting Struture by Employee Id.
        /// </summary>
        /// <param name="id">The Employee Id.</param>
        /// <returns>A fully prepped reporting structure if possible. Null if Id doesn't exist.</returns>
        public ReportingStructure GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                // Get the employee first.
                var employee = _employeeService.GetById(id);

                if (employee == null)
                    return null;

                // Create the Reporting Structure
                ReportingStructure reportingStructure = new ReportingStructure()
                {
                    Employee = (Employee)employee, //.Clone()
                    NumberOfReports = CountReports((Employee)employee)
                };

                return reportingStructure;
            }

            return null;
        }

        /// <summary>
        /// Counts the Direct Reports Recursively.
        /// </summary>
        /// <param name="employee">The given employee to start from.</param>
        /// <returns>The number of Reports found for the given employee.</returns>
        public static int CountReports(Employee employee)
        {
            int count = 0;
            if (employee.DirectReports != null)
            {
                foreach (var report in employee.DirectReports)
                {
                    // Count each employee we find before checking their reports
                    count++;
                    count += CountReports(report);
                }
            }
            return count;
        }
    }
}
