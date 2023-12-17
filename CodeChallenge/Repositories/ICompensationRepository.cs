using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// The interface that defines the Compensation Repository.
    /// </summary>
    /// <remarks>>
    /// Requirements: One to create and one to read by employeeId.
    /// </remarks>
    public interface ICompensationRepository
    {
        Compensation GetById(String id);
        Compensation Add(Compensation employee);
        Task SaveAsync();
    }
}
