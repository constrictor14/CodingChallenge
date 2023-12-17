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
    /// The Compensation Controller provides API access to the Compensation Service.
    /// </summary>
    /// <remarks>
    /// The Compensation should have its own route, and therefore controller.
    /// </remarks>
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService CompensationService)
        {
            _logger = logger;
            _compensationService = CompensationService;
        }

        /// <summary>
        /// This handles the API request and returns the Compensation of the given ID and their effective date.
        /// </summary>
        /// <param name="id">The Employee ID.</param>
        /// <returns>An IActionResult containing the Compensation object with the given ID if found, or NotFound if not found.</returns>
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received Compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("GetCompensationById", new { id = compensation.EmployeeId }, compensation);
        }
    }
}
