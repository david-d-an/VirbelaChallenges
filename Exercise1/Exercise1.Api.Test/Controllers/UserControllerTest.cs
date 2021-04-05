using System.Collections.Generic;
using Exercise1.Data.Repos;
using Exercise1.Data.Models.VirbelaListing;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;
using Exercise1.Api.Authentication.Provider;
using Microsoft.Extensions.Configuration;
using Exercise1.Data.Models.Authentication;
using Exercise1.Api.Authentication;
using Exercise1.Api.Common;
using System.Threading;

namespace Exercise1.Api.Controllers
{
    public class UserControllerShould {
        private Mock<ILogger<UserController>> mockLogger;
        private UserService userService;
        private CancellationToken cancellationToken;
        private UserController _controller;
        private IConfigurationRoot mockConfiguration;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IRepository<Listinguser>> mockListinguserRepository;

        public UserControllerShould()
        {
            mockLogger = new Mock<ILogger<UserController>>();
            mockConfiguration = Util.GetConfiguration();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockListinguserRepository = new Mock<IRepository<Listinguser>>();
            userService = new UserService(mockConfiguration, mockUnitOfWork.Object);

            cancellationToken = new CancellationToken();

            _controller = new UserController(
                mockLogger.Object, 
                mockConfiguration, 
                userService, 
                mockUnitOfWork.Object);
            // Need MockProblemDetails to be able to test with Problem() response type
            _controller.ProblemDetailsFactory = new MockProblemDetailsFactory();
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

            var expectedUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = Util.HashPassword(password),
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
            var result = await _controller.Login(loginModel, cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

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

        [Theory]
        [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5)]
        public async void ShouldDenyInvalidPassword(int id,
                                                    string userid, 
                                                    string email,
                                                    string firstname,
                                                    string lastname,            
                                                    string password,
                                                    int regionId) {
            // Arrange
            var userIdParam = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("UserId", userid)
            };
            var expectedUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = Util.HashPassword(password),
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
                Password = password + "abc"
            };
            var result = await _controller.Login(loginModel, cancellationToken);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, (result as UnauthorizedResult).StatusCode);
        }

        [Theory]
        [InlineData(1, "starburst", "starburst@example.com", "Star", "Burst", "test1", 5)]
        public async void ShouldDenyNonExistingUser(int id,
                                                    string userid, 
                                                    string email,
                                                    string firstname,
                                                    string lastname,            
                                                    string password,
                                                    int regionId) {
            // Arrange
            var userIdParam = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("UserId", userid)
            };
            var expectedUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = Util.HashPassword(password),
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
                .ReturnsAsync(new List<Listinguser>());
            mockUnitOfWork
                .Setup(uow => uow.ListinguserRepository)
                .Returns(mockListinguserRepository.Object);

            // Act
            var loginModel = new LoginModel {
                Userid = userid,
                Password = password
            };
            var result = await _controller.Login(loginModel, cancellationToken);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, (result as UnauthorizedResult).StatusCode);
        }

        [Theory]
        [InlineData(0, "starburst", "starburst@example.com", "Star", "Burst", "test1", 5)]
        public async void ShouldRegisterUser(int id,
                                             string userid, 
                                             string email,
                                             string firstname,
                                             string lastname,            
                                             string password,
                                             int regionId) {
            // Arrange
            var newUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = password,
                RegionId = regionId
            };
            var newUserRequest = new ListinguserRequest {
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = password,
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
                .ReturnsAsync(new List<Listinguser>());
            mockListinguserRepository
                .Setup(x => x.PostAsync(
                    It.Is<Listinguser>(x => x.Userid == userid)
                ))
                .ReturnsAsync(new Listinguser{
                    Id = 1,
                    Userid = userid,
                    Email = email,
                    Firstname = firstname,
                    Lastname = lastname,
                    Password = password,
                    RegionId = regionId
                });

            mockUnitOfWork
                .Setup(uow => uow.ListinguserRepository)
                .Returns(mockListinguserRepository.Object);

            // Act
            var result = await _controller.Post(newUserRequest, cancellationToken);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

            Assert.IsType<Listinguser>(createdResult.Value);
            var valueResult = createdResult.Value as Listinguser;
            Assert.NotNull(valueResult);

            // Assert.Equal(id, valueResult.Id);
            Assert.Equal(userid, valueResult.Userid);
            Assert.Equal(email, valueResult.Email);
            Assert.Equal(firstname, valueResult.Firstname);
            Assert.Equal(lastname, valueResult.Lastname);
            Assert.Equal(regionId, valueResult.RegionId);
        }

        [Theory]
        [InlineData(0, "starburst", "starburst@example.com", "Star", "Burst", "test1", 5)]
        public async void ShouldNotRegisterIfDuplicateUserIdFound(
            int id,
            string userid, 
            string email,
            string firstname,
            string lastname,            
            string password,
            int regionId)
        {
            // Arrange
            var newUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = password,
                RegionId = regionId
            };
            var newUserRequest = new ListinguserRequest {
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = password,
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
                .ReturnsAsync(new List<Listinguser>{ new Listinguser() });
            mockListinguserRepository
                .Setup(x => x.PostAsync(
                    It.Is<Listinguser>(x => x.Userid == userid)
                ))
                .ReturnsAsync(new Listinguser{
                    Id = 1,
                    Userid = userid,
                    Email = email,
                    Firstname = firstname,
                    Lastname = lastname,
                    Password = password,
                    RegionId = regionId
                });

            mockUnitOfWork
                .Setup(uow => uow.ListinguserRepository)
                .Returns(mockListinguserRepository.Object);

            // Act
            var result = await _controller.Post(newUserRequest, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var objResult = result as ObjectResult;
            Assert.Equal(500, objResult.StatusCode);

            var valueResult = objResult.Value as ProblemDetails;
            Assert.Contains("Registration Unsucessful", valueResult.Title);
            Assert.Contains("UserId already exists", valueResult.Detail);
        }

    }
}