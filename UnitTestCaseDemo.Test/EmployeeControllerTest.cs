using Microsoft.AspNetCore.Mvc;
using UnitTestCaseDemo.Controllers;
using UnitTestCaseDemo.Model;
using UnitTestCaseDemo.Services;

namespace UnitTestCaseDemo.Test
{
    public class EmployeeControllerTest
    {
        EmployeeController _controller;
        IEmployeeService _service;
        public EmployeeControllerTest()
        {
            _service = new EmployeeService();
            _controller = new EmployeeController(_service);
        }

        [Fact]
        public void GetAll_Employee_Success()
        {
            //Arrange

            //Act
            var result = _controller.Get();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<Employee>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Employee>>(resultType.Value);
            Assert.Equal(3, resultList.Count);
        }

        [Fact]
        public void GetById_Employee_Success()
        {
            //Arrange
            int valid_empid = 101;
            int invalid_empid = 110;

            //Act
            var errorResult = _controller.Get(invalid_empid);
            var successResult = _controller.Get(valid_empid);
            var successModel = successResult as OkObjectResult;
            var fetchedEmp = successModel.Value as Employee;

            //Assert
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<NotFoundResult>(errorResult);
            Assert.Equal(101, fetchedEmp.Id);
        }

        [Fact]
        public void Addd_Employee_Success()
        {
            //Arrange
            Employee employee = new Employee()
            {
                Id = 105,
                Name = "Shane Warne",
                PhoneNo = "55555555555",
                EmailId = "shane@email.com"
            };

            //Act
            var response = _controller.Post(employee);
            Assert.IsType<CreatedAtActionResult>(response);
            var createdEmp = response as CreatedAtActionResult;
            Assert.IsType<Employee>(createdEmp.Value);
            var employeeItem = createdEmp.Value as Employee;


            //Assert
            Assert.Equal("Shane Warne", employee.Name);
            Assert.Equal("55555555555", employeeItem.PhoneNo);
            Assert.Equal("shane@email.com", employeeItem.EmailId);
            
            
        }

        public void Delete_Employee_Success()
        {
            int valid_empid = 101;
            int invalid_empid = 110;

            var errorResult = _controller.Delete(invalid_empid);
            var successResult = _controller.Delete(valid_empid);

            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<NotFoundObjectResult>(errorResult);
        }
    }
}