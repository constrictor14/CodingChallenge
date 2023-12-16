using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    /// <summary>
    /// The Reporting Structure Serivce provides the scaffolding for future expansion in reporting.
    /// </summary>
    public interface IReportingStructureService
    {
        ReportingStructure GetById(String id);
    }
}
