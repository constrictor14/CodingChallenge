
using System;
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
    public class CompensationControllerTests
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
        /// Test to be sure the NotFound result is produced when we are unable to find the correct employee.
        /// </summary>
        [TestMethod]
        public void GetCompensationById_Returns_NotFound()
        {
            // Arrange
            var employeeId = "Invalid_Id";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Test to be sure the given Compensation is created.
        /// This includes the Employee, Salary, and EffectiveDate.
        /// </summary>
        [TestMethod]
        public void AddCompensation_Returns_Created()
        {
            // Add Compensation 
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                EffectiveDate = new DateTime(1940, 10, 09, 12, 0, 0),
                Salary = 19401980.40
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var responseAdd = postRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.Created, responseAdd.StatusCode);
        }

        /// <summary>
        /// Test to be sure the given Compensation is provided when given an employee id.
        /// This includes the Employee, Salary, and EffectiveDate.
        /// </summary>
        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Add Compensation 
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                EffectiveDate = new DateTime(1940, 07, 07, 12, 0, 0),
                Salary = 1940.83
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var responseAdd = postRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.Created, responseAdd.StatusCode);

            // Query For Compensation 
            // Arrange
            var employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f"; // Ringo
            var expectedCompensation = 1940.83;
            var expectedEffectiveDate = new DateTime(1940,07,07,12,0,0);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var responseGet = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseGet.StatusCode);
            var compensationResult = responseGet.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedCompensation, compensationResult.Salary);
            Assert.AreEqual(expectedEffectiveDate, compensationResult.EffectiveDate);
            Assert.AreEqual(employeeId, compensationResult.EmployeeId);

        }


    }
}
