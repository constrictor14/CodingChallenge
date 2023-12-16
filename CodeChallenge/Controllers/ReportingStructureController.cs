using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    /// <summary>
    /// The Reporting Structure Controller provides API access to the Reporting Service.
    /// </summary>
    /// <remarks>
    /// The Reporting Structure should have its own route, and therefore controller.
    /// </remarks>
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingService = reportingStructureService;
        }

        /// <summary>
        /// This handles the API request and returns the reporting structure of the given ID and count of their reports.
        /// </summary>
        /// <param name="id">The Employee ID.</param>
        /// <returns>An IActionResult containing the ReportingStructure object with the given ID if found, or NotFound if not found.</returns>
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            _logger.LogDebug($"Received ReportingStructure get request for '{id}'");

            var reportingStructure = _reportingService.GetById(id);

            if (reportingStructure == null)
                return NotFound();

            return Ok(reportingStructure);
        }
    }
}
