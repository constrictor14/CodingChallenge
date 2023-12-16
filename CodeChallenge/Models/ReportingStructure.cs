using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        /// <summary>
        /// Gets or sets the Employee for a specific reporting structure.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the total number of reports under a given employee.
        /// </summary>
        public double NumberOfReports { get; set; }

    }
}
