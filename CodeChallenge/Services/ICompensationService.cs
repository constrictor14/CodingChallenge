using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    /// <summary>
    /// The Compensation Serivce provides the scaffolding for future expansion into the compensation endpoint region.
    /// </summary>
    public interface ICompensationService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation compensation);
    }
}
