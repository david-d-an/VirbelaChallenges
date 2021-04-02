using Xunit;
using Exercise1.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Exercise1.Common.Security;
using Exercise1.DataAccess.Repos.VirbelaListing;
using System.Linq;
using System.Collections.Generic;
using Exercise1.Data.Models.VirbelaListing;
using System;

namespace Exercise1.DataAccess.Repos
{
    public class ListingRepositoryShould
    {
        private string connStr;
        private DbContextOptionsBuilder<VirbelaListingContext> dbOptionsbuilder;
        private VirbelaListingContext context;
        private UnitOfWork unitOfWork;

        public ListingRepositoryShould()
        {
            // ConnString for Dev Environment
            string encConnStr = 
                "RsJZctQGW8rsO2X/vhh7ewsDAKo8xDo7bEpjS7RwZFkq9KFLnlGEQLM9b3jGYARYVUINRxCTboYny3aWahtP7BHOew2ToMyxGDuO9BuYfpyZwH81uC883tyfXS2caR6rk0fTN1u/+dg05+L7sfLuDe8becDugt35NR2ahQEXCdVmHOs4JRAwWqvkL0EcqVVmwP4g1zUdfvg4yhzOtXVLmrf+xJFG6CFlCRw91hgUTCk5A6a2uPYHpKKiW7U0/cTZ6i9vKFqFJMvXxzRKU2hu3aMJ1iZsWgF3AR1jSwEOHQg=";
            connStr = AesCryptoUtil.Decrypt(encConnStr);
            dbOptionsbuilder = new DbContextOptionsBuilder<VirbelaListingContext>()
                                .UseSqlServer(connStr);
            context = new VirbelaListingContext(dbOptionsbuilder.Options);
            // unitOfWork = new UnitOfWork(context);
        }

        [Fact]
        public async void ReturnNoneForInvalidParameters()
        {
            // Arrange
            object invalidParams = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("CreatorId", "-1"),
                new KeyValuePair<string, string> ("Title", "" ),
                new KeyValuePair<string, string> ("Description", "" ),
                new KeyValuePair<string, string> ("Price", "-1")
            };

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(invalidParams);
                Assert.Equal(0, result.Count());
            }
        }

        [Fact]
        public async void ReturnNoneForInvalidId()
        {
            // Arrange
            string id = "-1";

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(id);
                Assert.Null(result);
            }
        }

        [Fact]
        public async void ReturnListingForValidId()
        {
            // Arrange
            string id = "1";

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(id);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void CreateListingButNotCommitWithoutError()
        {
            // Arrange
            string guid = new Guid().ToString();
            var listingCreateRquest = new Listing {
                Id = 0,
                Title = guid,
                Description = guid,
                Price = (decimal)999.99,
                CreatorId = 1,
                CreatedDate = DateTime.Now
            };
            object guidParams = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("Title", guid),
                new KeyValuePair<string, string> ("Description", guid)
            };

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.PostAsync(listingCreateRquest);
                Assert.NotNull(result);
            }
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(guidParams);
                Assert.Equal(0, result.Count());
            }
        }

    }

}