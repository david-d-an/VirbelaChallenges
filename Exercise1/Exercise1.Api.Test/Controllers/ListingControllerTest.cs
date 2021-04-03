using System.Collections.Generic;
using System.Linq;
using Exercise1.Data.Repos;
using Exercise1.Data.Models.VirbelaListing;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.Extensions.Logging;
using Exercise1.Api.Test.Helper;
using Microsoft.Extensions.Configuration;
using System;
using Exercise1.Api.Authentication;

namespace Exercise1.Api.Controllers
{
    public class ListingControllerTest {
        private Mock<ILogger<ListingController>> mockLogger;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private ListingController _controller;
        Mock<IRepository<Listing>> mockListingRepository;
        private Listinguser authenticatedUser;
        private CancellationToken cancellationToken;

        public ListingControllerTest()
        {
            mockLogger = new Mock<ILogger<ListingController>>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockListingRepository = new Mock<IRepository<Listing>>();

            authenticatedUser = new Listinguser {
                Id = 1,
                Userid = "jsmith",
                Email = "jsmith@example.com",
                Firstname = "John",
                Lastname = "Smith",
                Password = Helper.HashPassword("test1"),
                RegionId = 5
            };
            cancellationToken = new CancellationToken();

            _controller = new ListingController(
                mockLogger.Object,
                mockUnitOfWork.Object);
        }

        [Fact]
        public void ShouldDenyAnonymousUserViewingAll() {
            // Arrange

            // Act

            // Assert
            // var controllerType = _controller.GetType();
            // var methodInfo = controllerType.GetMethod("Get", new Type[] { 
            //     typeof(int?), 
            //     typeof(int?), 
            //     typeof(string), 
            //     typeof(string), 
            //     typeof(string), 
            //     typeof(string), 
            //     typeof(string), 
            //     typeof(CancellationToken)
            // });

            // var attributes = methodInfo.GetCustomAttributes(typeof(TokenAuthorizeAttribute), true);
            // Assert.True(attributes.Any(), "TokenAuthorizeAttribute found on ListingController.GetAll()");
        }

        [Fact]
        public void ShouldDenyAnonymousUserPosting() {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void ShouldDenyAnonymousUserEditing() {
            // Arrange

            // Act

            // Assert

        }


        [Theory]
        [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 3)]
        [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 7)]
        public async void ShouldReturnAllOfOwn(int id,
                                               string userid, 
                                               string email,
                                               string firstname,
                                               string lastname,            
                                               string password,
                                               int regionId,
                                               int listingCount) {
            // Arrange
            var user = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = Helper.HashPassword(password),
                RegionId = regionId
            };
            _controller.ControllerContext = Helper.GetControllerContext(user);

            var tmpListingList = new List<Listing>(listingCount);
            for (int i = 0; i < listingCount; i++)
                tmpListingList.Add(new Listing());

            mockListingRepository
                .Setup(x => x.GetAsync(
                    It.Is<List<KeyValuePair<string, string>>>(x => 
                        x.First(p => p.Key == "CreatorId").Value == user.Id.ToString()
                    ),
                    null,
                    null
                ))
                .ReturnsAsync(tmpListingList);
            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Get(
                null, null, null, null, null, null, null, cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var valueResult = okResult.Value as IEnumerable<Listing>;
            Assert.Equal(listingCount, valueResult.Count());
        }

        [Theory]
        [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5)]
        public async void ShouldReturnNoneIfNotOwningAny(int id,
                                                         string userid, 
                                                         string email,
                                                         string firstname,
                                                         string lastname,            
                                                         string password,
                                                         int regionId) {
            // Arrange
            var testingUser = new Listinguser {
                Id = id,
                Userid = userid,
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Password = Helper.HashPassword(password),
                RegionId = regionId
            };
            _controller.ControllerContext = Helper.GetControllerContext(testingUser);

            mockListingRepository
                .Setup(x => x.GetAsync(
                    It.Is<List<KeyValuePair<string, string>>>(x => 
                        x.First(p => p.Key == "CreatorId").Value == testingUser.Id.ToString()
                    ),
                    null,
                    null
                ))
                .ReturnsAsync(new List<Listing>());
            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Get(
                null, null, null, null, null, null, null, cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var valueResult = okResult.Value as IEnumerable<Listing>;
            Assert.Empty(valueResult);
        }

        [Theory]
        [InlineData(
            1,
            "New Title",
            "New Description",
            15.11,
            "03/21/2021 09:15:22 AM"
        )]
        public async void ShouldUpdateListingIfOwning(int id,
                                                      string title,
                                                      string description,
                                                      decimal price,
                                                      string createdDate) {
            // Arrange
            _controller.ControllerContext = 
                Helper.GetControllerContext(authenticatedUser);

            var updatedListing = new Listing {
                Id = id,
                Title = title,
                Description = description,
                Price = price,
                CreatorId = authenticatedUser.Id,
                CreatedDate = DateTime.Parse(createdDate)
            };

            mockListingRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == id.ToString())))
                .ReturnsAsync(new Listing{
                    Id = id,
                    Title = "Current Title",
                    Description = "Current Description",
                    Price = price,
                    CreatorId = authenticatedUser.Id,
                    CreatedDate = DateTime.Parse(createdDate)
                });

            mockListingRepository
                .Setup(x => x.PutAsync(
                    It.Is<string>(x => x == id.ToString()),
                    updatedListing
                ))
                .ReturnsAsync(updatedListing);

            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Put(
                id.ToString(), 
                updatedListing, 
                cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var valueResult = okResult.Value as Listing;
            Assert.NotNull(valueResult);

            Assert.Equal(updatedListing.Id, valueResult.Id);
            Assert.Equal(updatedListing.Title, valueResult.Title);
            Assert.Equal(updatedListing.Description, valueResult.Description);
            Assert.Equal(updatedListing.Price, valueResult.Price);
            Assert.Equal(updatedListing.CreatorId, valueResult.CreatorId);
            Assert.Equal(updatedListing.CreatedDate, valueResult.CreatedDate);
        }

        [Theory]
        [InlineData(
            1,
            "New Title",
            "New Description",
            15.11,
            "03/21/2021 09:15:22 AM"
        )]
        public async void ShouldNotUpdateIfNotOwning(int id,
                                                      string title,
                                                      string description,
                                                      decimal price,
                                                      string createdDate) {
            // Arrange
            _controller.ControllerContext = 
                Helper.GetControllerContext(authenticatedUser);

            var updatedListing = new Listing {
                Id = id,
                Title = title,
                Description = description,
                Price = price,
                CreatorId = authenticatedUser.Id,
                CreatedDate = DateTime.Parse(createdDate)
            };

            mockListingRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == id.ToString())))
                .ReturnsAsync(new Listing{
                    Id = id,
                    Title = "Current Title",
                    Description = "Current Description",
                    Price = price,
                    CreatorId = authenticatedUser.Id + 1,
                    CreatedDate = DateTime.Parse(createdDate)
                });

            mockListingRepository
                .Setup(x => x.PutAsync(
                    It.Is<string>(x => x == id.ToString()),
                    updatedListing
                ))
                .ReturnsAsync(updatedListing);

            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Put(
                id.ToString(), 
                updatedListing, 
                cancellationToken);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, (result as UnauthorizedResult).StatusCode);
        }

        [Theory]
        [InlineData(
            "New Title",
            "New Description",
            15.11
        )]
        public async void ShouldCreateListing(string title,
                                              string description,
                                              decimal price) {
            // Arrange
            DateTime createdDate = DateTime.Now;
            _controller.ControllerContext = 
                Helper.GetControllerContext(authenticatedUser);

            var newListing = new Listing {
                Id = 0,
                Title = title,
                Description = description,
                Price = price,
                CreatorId = authenticatedUser.Id,
                CreatedDate = createdDate
            };

            mockListingRepository
                .Setup(x => x.PostAsync(
                    It.Is<Listing>(x => 
                        x.Title == newListing.Title &&
                        x.Description == newListing.Description &&
                        x.Price == newListing.Price &&
                        x.CreatorId == newListing.CreatorId &&
                        x.CreatedDate == newListing.CreatedDate
                    )
                ))
                .ReturnsAsync(newListing);

            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Post(
                newListing, 
                cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var valueResult = okResult.Value as Listing;
            Assert.NotNull(valueResult);

            // Assert.Equal(updatedListing.Id, valueResult.Id);
            // Assert.Equal(updatedListing.Title, valueResult.Title);
            // Assert.Equal(updatedListing.Description, valueResult.Description);
            // Assert.Equal(updatedListing.Price, valueResult.Price);
            // Assert.Equal(updatedListing.CreatorId, valueResult.CreatorId);
            // Assert.Equal(updatedListing.CreatedDate, valueResult.CreatedDate);
        }

    }
}