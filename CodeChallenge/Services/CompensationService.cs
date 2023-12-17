using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    /// <summary>
    /// The Compensation Service provides a separation of business logic from the associated controller.
    /// </summary>
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new compensation entry in the compensation repository.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        /// <summary>
        /// This Gets the Compensation by Employee Id.
        /// </summary>
        /// <param name="id">The Employee Id.</param>
        /// <returns>A Compensation if possible. Null if Id doesn't exist.</returns>
        public Compensation GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }


    }
}
