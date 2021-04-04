// using System.Collections.Generic;
// using System.Linq;
// using Exercise1.Data.Repos;
// using Exercise1.Data.Models.VirbelaListing;
// using Moq;
// using Xunit;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Threading;
// using Microsoft.Extensions.Logging;
// using Exercise1.Api.Common;
// using System.Threading.Tasks;

// namespace Exercise1.Api.Controllers
// {
//     public class RegionalListingControllerTest {
//        private Mock<ILogger<RegionalListingController>> mockLogger;
//         private Mock<IUnitOfWork> mockUnitOfWork;
//         private RegionalListingController _controller;
//         Mock<IRepository<Region_Listing>> mockRegion_ListingRepository;
//         private Listinguser authenticatedUser;
//         // private CancellationToken cancellationToken;

//         public RegionalListingControllerTest()
//         {
//             mockLogger = new Mock<ILogger<RegionalListingController>>();
//             mockUnitOfWork = new Mock<IUnitOfWork>();
//             mockRegion_ListingRepository = new Mock<IRepository<Region_Listing>>();

//             authenticatedUser = new Listinguser {
//                 Id = 1,
//                 Userid = "jsmith",
//                 Email = "jsmith@example.com",
//                 Firstname = "John",
//                 Lastname = "Smith",
//                 Password = Util.HashPassword("test1"),
//                 RegionId = 5
//             };
//             // cancellationToken = new CancellationToken();

//             _controller = new RegionalListingController(
//                 mockLogger.Object,
//                 mockUnitOfWork.Object);
//         }

//         [Theory]
//         [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 3)]
//         [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 7)]
//         public async void ShouldReturnAllOfOwn(int id,
//                                                string userid, 
//                                                string email,
//                                                string firstname,
//                                                string lastname,            
//                                                string password,
//                                                int regionId,
//                                                int listingCount) {
//             // Arrange
//             var user = new Listinguser {
//                 Id = id,
//                 Userid = userid,
//                 Email = email,
//                 Firstname = firstname,
//                 Lastname = lastname,
//                 Password = Util.HashPassword(password),
//                 RegionId = regionId
//             };
//             _controller.ControllerContext = Util.GetControllerContext(user);

//             var tmpListingList = new List<Listing>(listingCount);
//             for (int i = 0; i < listingCount; i++)
//                 tmpListingList.Add(new Listing());

//             // mockListingRepository
//             //     .Setup(x => x.GetAsync(
//             //         It.Is<List<KeyValuePair<string, string>>>(x => 
//             //             x.First(p => p.Key == "CreatorId").Value == user.Id.ToString()
//             //         ),
//             //         null,
//             //         null
//             //     ))
//             //     .ReturnsAsync(tmpListingList);
//             // mockUnitOfWork
//             //     .Setup(uow => uow.ListingRepository)
//             //     .Returns(mockListingRepository.Object);

//             // Act
//             await Task.Delay(0);
//             // var result = await _controller.Get(
//             //     null, null, null, null, null, null, null, cancellationToken);

//             // // Assert
//             // var okResult = result as OkObjectResult;
//             // Assert.NotNull(okResult);
//             // Assert.Equal(200, okResult.StatusCode);

//             // var valueResult = okResult.Value as IEnumerable<Listing>;
//             // Assert.Equal(listingCount, valueResult.Count());
//         }

//         [Theory]
//         [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 3)]
//         [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5, 7)]
//         public async void ShouldReturnAllInSameRetion(int id,
//                                                       string userid, 
//                                                       string email,
//                                                       string firstname,
//                                                       string lastname,            
//                                                       string password,
//                                                       int regionId,
//                                                       int listingCount) {
//             // Arrange
//             var user = new Listinguser {
//                 Id = id,
//                 Userid = userid,
//                 Email = email,
//                 Firstname = firstname,
//                 Lastname = lastname,
//                 Password = Util.HashPassword(password),
//                 RegionId = regionId
//             };
//             _controller.ControllerContext = Util.GetControllerContext(user);

//             var tmpListingList = new List<Listing>(listingCount);
//             for (int i = 0; i < listingCount; i++)
//                 tmpListingList.Add(new Listing());

//             // mockListingRepository
//             //     .Setup(x => x.GetAsync(
//             //         It.Is<List<KeyValuePair<string, string>>>(x => 
//             //             x.First(p => p.Key == "CreatorId").Value == user.Id.ToString()
//             //         ),
//             //         null,
//             //         null
//             //     ))
//             //     .ReturnsAsync(tmpListingList);
//             // mockUnitOfWork
//             //     .Setup(uow => uow.ListingRepository)
//             //     .Returns(mockListingRepository.Object);

//             // Act
//             await Task.Delay(0);
//             // var result = await _controller.Get(
//             //     null, null, null, null, null, null, null, cancellationToken);

//             // // Assert
//             // var okResult = result as OkObjectResult;
//             // Assert.NotNull(okResult);
//             // Assert.Equal(200, okResult.StatusCode);

//             // var valueResult = okResult.Value as IEnumerable<Listing>;
//             // Assert.Equal(listingCount, valueResult.Count());
//         }

//         [Theory]
//         [InlineData(1, "jsmith", "jsmith@example.com", "John", "Smith", "test1", 5)]
//         public async void ShouldReturnNoneIfNotOwningAny(int id,
//                                                          string userid, 
//                                                          string email,
//                                                          string firstname,
//                                                          string lastname,            
//                                                          string password,
//                                                          int regionId) {
//             // Arrange
//             var testingUser = new Listinguser {
//                 Id = id,
//                 Userid = userid,
//                 Email = email,
//                 Firstname = firstname,
//                 Lastname = lastname,
//                 Password = Util.HashPassword(password),
//                 RegionId = regionId
//             };
//             _controller.ControllerContext = Util.GetControllerContext(testingUser);

//             // mockListingRepository
//             //     .Setup(x => x.GetAsync(
//             //         It.Is<List<KeyValuePair<string, string>>>(x => 
//             //             x.First(p => p.Key == "CreatorId").Value == testingUser.Id.ToString()
//             //         ),
//             //         null,
//             //         null
//             //     ))
//             //     .ReturnsAsync(new List<Listing>());
//             // mockUnitOfWork
//             //     .Setup(uow => uow.ListingRepository)
//             //     .Returns(mockListingRepository.Object);

//             // Act
//             await Task.Delay(0);
//             // var result = await _controller.Get(
//             //     null, null, null, null, null, null, null, cancellationToken);

//             // // Assert
//             // var okResult = result as OkObjectResult;
//             // Assert.NotNull(okResult);
//             // Assert.Equal(200, okResult.StatusCode);

//             // var valueResult = okResult.Value as IEnumerable<Listing>;
//             // Assert.Empty(valueResult);
//         }

//     }
// }