using System.Collections.Generic;
using System.Linq;
using Exercise1.Data.Repos;
using Exercise1.Data.Models.VirbelaListing;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.Extensions.Logging;
using Exercise1.Api.Common;
using System;

namespace Exercise1.Api.Controllers
{
    public class ListingControllerShould {
        private Mock<ILogger<ListingController>> mockLogger;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private ListingController _controller;
        Mock<IRepository<Listing>> mockListingRepository;
        private Mock<IRepository<Listinguser>> mockListinguserRepository;
        private Mock<IRepository<Region_Listing>> mockRegion_ListingRepository;
        private Listinguser authenticatedUser;
        private CancellationToken cancellationToken;

        public ListingControllerShould()
        {
            mockLogger = new Mock<ILogger<ListingController>>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockListingRepository = new Mock<IRepository<Listing>>();
            mockListinguserRepository = new Mock<IRepository<Listinguser>>();
            mockRegion_ListingRepository = new Mock<IRepository<Region_Listing>>();

            authenticatedUser = new Listinguser {
                Id = 1,
                Userid = "jsmith",
                Email = "jsmith@example.com",
                Firstname = "John",
                Lastname = "Smith",
                Password = Util.HashPassword("test1"),
                RegionId = 5
            };
            cancellationToken = new CancellationToken();

            _controller = new ListingController(
                mockLogger.Object,
                mockUnitOfWork.Object);
        }

        [Fact]
        public void DenyAnonymousUserViewingAll() {
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
        public void DenyAnonymousUserPosting() {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void DenyAnonymousUserEditing() {
            // Arrange

            // Act

            // Assert

        }

        // [Theory]
        // [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 3)]
        // [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 7)]
        // public async void ShouldReturnAllOfOwn(int id,
        //                                        string userid, 
        //                                        string email,
        //                                        string firstname,
        //                                        string lastname,            
        //                                        string password,
        //                                        int regionId,
        //                                        int listingCount) {
        //     // Arrange
        //     var user = new Listinguser {
        //         Id = id,
        //         Userid = userid,
        //         Email = email,
        //         Firstname = firstname,
        //         Lastname = lastname,
        //         Password = Util.HashPassword(password),
        //         RegionId = regionId
        //     };
        //     _controller.ControllerContext = Util.GetControllerContext(user);

        //     var tmpListingList = new List<Listing>(listingCount);
        //     for (int i = 0; i < listingCount; i++)
        //         tmpListingList.Add(new Listing());

        //     mockListingRepository
        //         .Setup(x => x.GetAsync(
        //             It.Is<List<KeyValuePair<string, string>>>(x => 
        //                 x.First(p => p.Key == "CreatorId").Value == user.Id.ToString()
        //             ),
        //             null,
        //             null
        //         ))
        //         .ReturnsAsync(tmpListingList);
        //     mockUnitOfWork
        //         .Setup(uow => uow.ListingRepository)
        //         .Returns(mockListingRepository.Object);

        //     // Act
        //     var result = await _controller.Get(
        //         null, null, null, null, null, null, cancellationToken);

        //     // Assert
        //     var okResult = result as OkObjectResult;
        //     Assert.NotNull(okResult);
        //     Assert.Equal(200, okResult.StatusCode);

        //     var valueResult = okResult.Value as IEnumerable<Listing>;
        //     Assert.Equal(listingCount, valueResult.Count());
        // }

        // [Theory]
        // [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5)]
        // public async void ShouldReturnNoneIfNotOwningAny(int id,
        //                                                  string userid, 
        //                                                  string email,
        //                                                  string firstname,
        //                                                  string lastname,            
        //                                                  string password,
        //                                                  int regionId) {
        //     // Arrange
        //     var testingUser = new Listinguser {
        //         Id = id,
        //         Userid = userid,
        //         Email = email,
        //         Firstname = firstname,
        //         Lastname = lastname,
        //         Password = Util.HashPassword(password),
        //         RegionId = regionId
        //     };
        //     _controller.ControllerContext = Util.GetControllerContext(testingUser);

        //     mockListingRepository
        //         .Setup(x => x.GetAsync(
        //             It.Is<List<KeyValuePair<string, string>>>(x => 
        //                 x.First(p => p.Key == "CreatorId").Value == testingUser.Id.ToString()
        //             ),
        //             null,
        //             null
        //         ))
        //         .ReturnsAsync(new List<Listing>());
        //     mockUnitOfWork
        //         .Setup(uow => uow.ListingRepository)
        //         .Returns(mockListingRepository.Object);

        //     // Act
        //     var result = await _controller.Get(
        //         null, null, null, null, null, null, cancellationToken);

        //     // Assert
        //     var okResult = result as OkObjectResult;
        //     Assert.NotNull(okResult);
        //     Assert.Equal(200, okResult.StatusCode);

        //     var valueResult = okResult.Value as IEnumerable<Listing>;
        //     Assert.Empty(valueResult);
        // }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(15, 50)]
        [InlineData(15, 500)]
        public async void ReturnAllInOwnRegionOnly(
            int regionCount, int listingCount) {
            // Arrange
            _controller.ControllerContext = 
                Util.GetControllerContext(authenticatedUser);

            Random random = new Random(Guid.NewGuid().GetHashCode());

            var tmpRegion_ListingList = new List<Region_Listing>(listingCount);
            for (int i = 0; i < listingCount; i++)
                tmpRegion_ListingList.Add(new Region_Listing{
                    RegionId = random.Next(1, regionCount + 1)
                });

            int countSameRegion = 
                tmpRegion_ListingList
                .Where(i => i.RegionId == authenticatedUser.RegionId)
                .Count();

            mockListinguserRepository
                .Setup(x => x.GetAsync(
                    It.Is<string>(x => x == authenticatedUser.Id.ToString())
                ))
                .ReturnsAsync(authenticatedUser);
            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            mockRegion_ListingRepository
                .Setup(x => x.GetAsync(
                    It.Is<List<KeyValuePair<string, string>>>(x => 
                        x.First(p => p.Key == "RegionId")
                        .Value == authenticatedUser.RegionId.ToString()
                    ),
                    null,
                    null
                ))
                .ReturnsAsync(
                    tmpRegion_ListingList
                    .Where(i => i.RegionId == authenticatedUser.RegionId)
                );
            mockUnitOfWork
                .Setup(uow => uow.Region_ListingRepository)
                .Returns(mockRegion_ListingRepository.Object);

            // Act
            var result = await _controller.Get(
                null, null, null, null, null, null, cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var valueResult = okResult.Value as IEnumerable<Region_Listing>;
            // Count listings in user's region
            int inRegionCount = valueResult
                            .Where(i => i.RegionId == authenticatedUser.RegionId)
                            .Count();
            // Count listings notin user's region
            int outRegionCount = valueResult
                            .Where(i => i.RegionId != authenticatedUser.RegionId)
                            .Count();

            // Check if the initial in-region count matches the returned list.
            Assert.Equal(countSameRegion, inRegionCount);
            // Check if out-of-region count is 0.
            Assert.Equal(0, outRegionCount);
        }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(15, 50)]
        [InlineData(15, 500)]
        public async void ReturnListingInOwnRegionOnly(
            int regionCount, int listingCount) {
            // Arrange
            _controller.ControllerContext = 
                Util.GetControllerContext(authenticatedUser);

            Random random = new Random(Guid.NewGuid().GetHashCode());

            var tmpRegion_ListingList = new List<Region_Listing>(listingCount);
            for (int i = 0; i < listingCount; i++)
                tmpRegion_ListingList.Add(new Region_Listing{
                    Id = i + 1,
                    RegionId = random.Next(1, regionCount + 1)
                });

            var listingSameRegion = 
                tmpRegion_ListingList
                .Where(i => i.RegionId == authenticatedUser.RegionId);

            mockListinguserRepository
                .Setup(x => x.GetAsync(
                    It.Is<string>(x => x == authenticatedUser.Id.ToString())
                ))
                .ReturnsAsync(authenticatedUser);
            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);


            foreach(var r in tmpRegion_ListingList) {
                mockRegion_ListingRepository
                    .Setup(x => x.GetAsync(
                        It.Is<string>(x => x == r.Id.ToString())
                    ))
                    .ReturnsAsync(
                        tmpRegion_ListingList
                        .Where(i => i.Id == r.Id)
                        .FirstOrDefault()
                    );
            }
            mockUnitOfWork
                .Setup(uow => uow.Region_ListingRepository)
                .Returns(mockRegion_ListingRepository.Object);

            foreach(var r in tmpRegion_ListingList) {
                // Act
                var result = await _controller.Get(r.Id, cancellationToken);
                Assert.IsType<OkObjectResult>(result);
                var okResult = result as OkObjectResult;
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                var valueResult = okResult.Value as Region_Listing;
                // Assert
                if (r.RegionId == authenticatedUser.RegionId) {
                    Assert.NotNull(valueResult);
                    Assert.Equal(r.Id, valueResult.Id);
                    Assert.Equal(r.RegionId, valueResult.RegionId);
                }
                else {
                    Assert.Null(valueResult);
                }
            }
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
                Util.GetControllerContext(authenticatedUser);

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
                Util.GetControllerContext(authenticatedUser);

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
                Util.GetControllerContext(authenticatedUser);

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
            var createdResult = result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

            Assert.IsType<Listing>(createdResult.Value);
            var valueResult = createdResult.Value as Listing;
            Assert.NotNull(valueResult);

            // Assert.Equal(newListing.Id, valueResult.Id);
            Assert.Equal(newListing.Title, valueResult.Title);
            Assert.Equal(newListing.Description, valueResult.Description);
            Assert.Equal(newListing.Price, valueResult.Price);
            Assert.Equal(newListing.CreatorId, valueResult.CreatorId);
            Assert.Equal(newListing.CreatedDate, valueResult.CreatedDate);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void ShouldDeleteIfOwning(int id) {
            _controller.ControllerContext = 
                Util.GetControllerContext(authenticatedUser);

            var listingToDelete = new Listing {
                    Id = id,
                    Title = "Delete Title",
                    Description = "Delete This",
                    Price = new Random().Next(1, 10),
                    CreatorId = authenticatedUser.Id,
                    CreatedDate = DateTime.Now
                };

            mockListingRepository
                .Setup(x => x.GetAsync(
                    It.Is<string>(x => x == id.ToString()))
                )
                .ReturnsAsync(listingToDelete);

            mockListingRepository
                .Setup(x => x.DeleteAsync(
                    It.Is<string>(x => x == id.ToString()))
                )
                .ReturnsAsync(listingToDelete);

            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Delete(id, cancellationToken);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var valueResult = okResult.Value as Listing;
            Assert.Equal(listingToDelete.Id, valueResult.Id);
            Assert.Equal(listingToDelete.Title, valueResult.Title);
            Assert.Equal(listingToDelete.Description, valueResult.Description);
            Assert.Equal(listingToDelete.Price, valueResult.Price);
            Assert.Equal(listingToDelete.CreatorId, valueResult.CreatorId);
            Assert.Equal(listingToDelete.CreatedDate, valueResult.CreatedDate);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void ShouldNotDeleteIfNotOwning(int id) {
            _controller.ControllerContext = 
                Util.GetControllerContext(authenticatedUser);

            var listingToDelete = new Listing {
                    Id = id,
                    Title = "Don't Delete",
                    Description = "Delete This Not",
                    Price = new Random().Next(1, 10),
                    CreatorId = authenticatedUser.Id + 1,
                    CreatedDate = DateTime.Now
                };

            mockListingRepository
                .Setup(x => x.GetAsync(
                    It.Is<string>(x => x == id.ToString()))
                )
                .ReturnsAsync(listingToDelete);

            mockListingRepository
                .Setup(x => x.DeleteAsync(
                    It.Is<string>(x => x == id.ToString()))
                )
                .ReturnsAsync(listingToDelete);

            mockUnitOfWork
                .Setup(uow => uow.ListingRepository)
                .Returns(mockListingRepository.Object);

            // Act
            var result = await _controller.Delete(id, cancellationToken);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, (result as UnauthorizedResult).StatusCode);
        }

    }
}