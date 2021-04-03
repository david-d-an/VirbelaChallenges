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
using Exercise1.Api.Test.Helper;
using System.Threading.Tasks;

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
            mockConfiguration = Helper.GetConfiguration();
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

            var expectedUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = Helper.HashPassword(password),
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
                Password = Helper.HashPassword(password),
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
            var result = await _controller.Login(loginModel);

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
                Password = Helper.HashPassword(password),
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
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, (result as UnauthorizedResult).StatusCode);
        }


        [Fact]
        public async void ShouldRegisterUser() {
            // Arrange
            await Task.Delay(0);
            // Act

            // Assert
        }

    }
}