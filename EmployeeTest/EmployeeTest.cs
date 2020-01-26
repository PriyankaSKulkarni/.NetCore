using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace EmployeeTest
{
    [TestClass]
    public class EmployeeTest
    {
        private Mock<IRepository<Employee>> _mockEmployeeRepository;
        private readonly Mock<IHostingEnvironment> hostingEnvironment;

        public EmployeeTest()
        {
            _mockEmployeeRepository = new Mock<IRepository<Employee>>();
            this.hostingEnvironment = new Mock<IHostingEnvironment>();
        }
        [TestMethod]
        public void Employee_GetDetails()
        {
            //Arrange
            HomeController homeController = new HomeController(_mockEmployeeRepository.Object, hostingEnvironment.Object);
            _mockEmployeeRepository.Setup(i => i.GetEmployees(1)).Returns(Employees());
            //Act
            var result = homeController.Details(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model); // add additional checks on the Model
            //Assert.IsTrue(string.IsNullOrEmpty(result.pagetitle) || result.ViewName == "Details");
        }

        private List<Employee> Employees() {
            return new List<Employee> {
                new Employee() { Id = 1, EmployeeName = "ABC", Email = "abc@gmail.com", Address = Address.Mumbai }
            };
        }
    }
}
