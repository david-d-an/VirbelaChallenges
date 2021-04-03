using System.Collections.Generic;
using Exercise1.Data.Repos;
using Exercise1.Data.Models.VirbelaListing;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;
using Exercise1.Api.Authentication.Provider;
using System.IO;
using Microsoft.Extensions.Configuration;
using Exercise1.Data.Models.Authentication;
using Exercise1.Api.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Exercise1.Api.Controllers
{
    public class UserControllerShould {
        private Mock<ILogger<UserController>> mockLogger;
        private UserService userService;
        private UserController _controller;
        private IConfigurationRoot mockConfiguration;
        private Mock<IUnitOfWork> mockUnitOfWork;

        public UserControllerShould()
        {
            mockLogger = new Mock<ILogger<UserController>>();

            string appsettingDirectory = 
                Directory
                .GetCurrentDirectory()
                .Replace(".Test/bin/Debug/netcoreapp3.1", "");
            mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(appsettingDirectory)
                .AddJsonFile("appsettings.Development.json")
                .Build();

            mockUnitOfWork = new Mock<IUnitOfWork>();
            userService = new UserService(mockConfiguration, mockUnitOfWork.Object);

            _controller = new UserController(
                mockLogger.Object, 
                mockConfiguration, 
                userService, 
                mockUnitOfWork.Object);
        }

        [Theory]
        [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5)]
        [InlineData(24, "kwest", "kwest@example.com", "Kanye", "West", "test2", 25)]
        public async void ShouldLoginValidCredential(int id,
                                                     string userid, 
                                                     string email,
                                                     string firstname,
                                                     string lastname,            
                                                     string password,
                                                     int regionId) {
            // Arrange
            Mock<IRepository<Listinguser>> mockListinguserRepository = 
                new Mock<IRepository<Listinguser>>();

            var userIdParam = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("UserId", userid)
            };
            #nullable enable
            var hashedPassword = 
                new PasswordHasher<object?>()
                .HashPassword(null, password);
            #nullable disable

            var expectedUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = hashedPassword,
                RegionId = regionId
            };

            mockListinguserRepository
                .Setup(x => x.GetAsync(
                    It.Is<List<KeyValuePair<string, string>>>(x => 
                        x.First(p => p.Key == "UserId").Value == userid
                    ),
                    null,
                    null
                ))
                .ReturnsAsync(new List<Listinguser> {
                    expectedUser
                });
            mockUnitOfWork
                .Setup(uow => uow.ListinguserRepository)
                .Returns(mockListinguserRepository.Object);

            // Act
            var loginModel = new LoginModel {
                Userid = userid,
                Password = password
            };
            var result = await _controller.Login(loginModel);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var authenticateResponse = okResult.Value as AuthenticateResponse;
            Assert.NotNull(authenticateResponse);
            Assert.False(string.IsNullOrEmpty(authenticateResponse.Token));

            Assert.Equal(id, authenticateResponse.Id);
            Assert.Equal(userid, authenticateResponse.Userid);
            Assert.Equal(email, authenticateResponse.Email);
            Assert.Equal(firstname, authenticateResponse.Firstname);
            Assert.Equal(lastname, authenticateResponse.Lastname);
            Assert.Equal(regionId, authenticateResponse.RegionId);
        }

        [Fact]
        public async void ShouldDenyInvalidCredential() {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async void ShouldRegisterUser() {
            // Arrange

            // Act

            // Assert
        }


        //     mockEmployeeRepository.Setup(x => x.GetAsync(null, null, null)).Returns(employees);

        //     // Act
        //     ActionResult<IEnumerable<Employees>> searchResult = await _controller.Get();
        //     OkObjectResult listResult = searchResult.Result as OkObjectResult;

        //     // Assert
        //     Assert.Equal(200, listResult.StatusCode);
        //     IEnumerable<Employees> list = listResult.Value as IEnumerable<Employees>;
        //     Assert.Single(list);
        //     Assert.NotNull(list.FirstOrDefault());
        //     Assert.Equal(empNo, list.FirstOrDefault().EmpNo);
        // }

        //         [Fact]
        // public async void ShouldReturnEmployeeWithEmployeeNumber()
        // {
        //     // Arrange

        //     // Act
        //     ActionResult<Employees> searchResult = await _controller.Get(empNo);

        //     // Assert
        //     Assert.Equal(empNo, searchResult.Value.EmpNo);
        // }

        // [Fact]
        // public async void ShouldReturnNoEmployeesWithInvalidEmpNo() {
        //     // Arrange

        //     // Act
        //     ActionResult<Employees> searchResult = await _controller.Get(invalidEmpNo);

        //     // Assert
        //     Assert.Null(searchResult.Value);
        // }

        // [Fact]
        // public async void ShouldUpdateEmployeeInfo()
        // {
        //     // Arrange
        //     Employees employeeUpdateRequest = 
        //         new Employees {
        //             EmpNo = empNo,
        //             BirthDate = DateTime.Now.AddDays(-1000),
        //             FirstName = "Jane",
        //             LastName = "Doe",
        //             Gender = "Female",
        //             HireDate = DateTime.Now.AddDays(-1000)
        //         };

        //     mockEmployeeRepository.Setup(x => x.PutAsync(empNo.ToString(), employeeUpdateRequest))
        //                           .ReturnsAsync(employeeUpdateRequest);

        //     // // Pre Assert
        //     // ActionResult<Employees> existingEmployee = await _controller.Get(empNo);
        //     // // Assert.NotNull(existingEmployee.Result);
        //     // Assert.NotNull(existingEmployee.Value);

        //     // Act
        //     ActionResult<Employees> updateResult = await _controller.Put(empNo, employeeUpdateRequest);
        //     NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;

        //     // Assert
        //     Assert.Null(notFoundResult);
        //     Assert.NotNull(updateResult.Value);
        //     Assert.Equal(employeeUpdateRequest.EmpNo, updateResult.Value.EmpNo);
        //     Assert.Equal(employeeUpdateRequest.BirthDate, updateResult.Value.BirthDate);
        //     Assert.Equal(employeeUpdateRequest.FirstName, updateResult.Value.FirstName);
        //     Assert.Equal(employeeUpdateRequest.LastName, updateResult.Value.LastName);
        //     Assert.Equal(employeeUpdateRequest.Gender, updateResult.Value.Gender);
        //     Assert.Equal(employeeUpdateRequest.HireDate, updateResult.Value.HireDate);
        // }
 
        //  [Fact]
        // public async void ShouldCreateEmployee()
        // {
        //     // Arrange
        //     EmployeeRequest employeeCreateRequest = 
        //         new EmployeeRequest {
        //             EmpNo = null,
        //             BirthDate = DateTime.MinValue,
        //             FirstName = "John",
        //             LastName = "Smith",
        //             Gender = "Male",
        //             HireDate = DateTime.MinValue
        //         };

        //     Employees newEmployee = 
        //         new Employees {
        //             EmpNo = newEmpNo,
        //             BirthDate = DateTime.MinValue,
        //             FirstName = "John",
        //             LastName = "Smith",
        //             Gender = "Male",
        //             HireDate = DateTime.MinValue
        //         };

        //     mockEmployeeRepository
        //         .Setup(x => x.PostAsync(It.Is<EmployeeRequest>(x => x == employeeCreateRequest)))
        //         .ReturnsAsync(newEmployee);

        //     // Pre Assert
        //     ActionResult<Employees> existingEmployee = _controller.Get(newEmpNo).Result;
        //     Assert.Null(existingEmployee.Value);

        //     // Act
        //     ActionResult<Employees> postResult = await _controller.Post(employeeCreateRequest);
        //     CreatedAtActionResult createdResult = postResult.Result as CreatedAtActionResult;
        //     Employees createdEmployee = createdResult.Value as Employees;
            
        //     // Assert
        //     Assert.NotNull(createdResult);
        //     Assert.Equal(201, createdResult.StatusCode);
        //     // Assert.NotEqual(createResult.EmpNo, employeeCreateRequest.EmpNo);
        //     Assert.Equal(employeeCreateRequest.BirthDate, createdEmployee.BirthDate);
        //     Assert.Equal(employeeCreateRequest.FirstName, createdEmployee.FirstName);
        //     Assert.Equal(employeeCreateRequest.LastName, createdEmployee.LastName);
        //     Assert.Equal(employeeCreateRequest.Gender, createdEmployee.Gender);
        //     Assert.Equal(employeeCreateRequest.HireDate, createdEmployee.HireDate);
        // }

        // [Fact]
        // public async void ShouldDeleteEmployee()
        // {
        //     // Arrange
        //     mockEmployeeRepository.Setup(x => x.DeleteAsync(empNo.ToString()))
        //                           .ReturnsAsync((Employees)null);

        //     // Act
        //     ActionResult<Employees> deleteResult = await _controller.Delete(empNo);

        //     // Assert
        //     NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;
        //     Assert.Null(notFoundResult);
        //     Assert.Null(deleteResult.Value);
        // } 

        // [Fact]
        // public async void ShouldReturnNotFoundWhenDeleteEmployeeWithInvalidEmpNo()
        // {
        //     // Arrange

        //     // Act
        //     ActionResult<Employees> deleteResult = await _controller.Delete(invalidEmpNo);
        //     NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;

        //     // Assert
        //     Assert.Equal(404, notFoundResult.StatusCode);
        // }


    }
}