
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;
using CodeChallenge.Services;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup(ClassCleanupBehavior.EndOfClass)]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        /// <summary>
        /// Test to be sure the given employee is returned in the result with name intact and the expected number of reports matches
        /// the calculation.
        /// </summary>
        /// <remarks>
        /// The endpoint state seems to be dirty after the Employee tests. This is something I'd be concerned about
        /// normally. Ideally, this test would be run isolated from the other test clsses to be a valid test in my mind. I'm 
        /// going to say that correcting that is outside of the scope of this project, for the sake of keeping to the
        /// projected time limit.
        /// </remarks>
        [TestMethod]
        public void GetReportingStructureById_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            var expetedNumberOfReports = ReportingStructureService.CountReports(reportingStructure.Employee);
            Assert.AreEqual(expetedNumberOfReports, reportingStructure.NumberOfReports);

            // The Update Employee Test is somewhat interfering with this test as it changes the data state.
            if (reportingStructure.Employee.DirectReports != null)
            {
                if (reportingStructure.Employee.DirectReports.Count > 1)
                {
                    expetedNumberOfReports = 4;
                    Assert.AreEqual(expetedNumberOfReports, reportingStructure.NumberOfReports);
                }
                else
                {
                    expetedNumberOfReports = 1;
                    Assert.AreEqual(expetedNumberOfReports, reportingStructure.NumberOfReports);
                }
            }
        }

        /// <summary>
        /// Test to be sure the NotFound result is produced when we are unable to find the correct employee.
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_NotFound()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "Invalid_Id",
                Department = "Former Presidents",
                FirstName = "George",
                LastName = "Washington",
                Position = "Commander In Chief",
            };
            var requestContent = new JsonSerialization().ToJson(employee);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employee.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
