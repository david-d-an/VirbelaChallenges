using Xunit;
using Exercise1.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Exercise1.Common.Security;
using Exercise1.DataAccess.Repos.VirbelaListing;
using System.Collections.Generic;

namespace Exercise1.DataAccess.Repos
{
    public class RegionRepositoryShould
    {
        private string connStr;
        private DbContextOptionsBuilder<VirbelaListingContext> dbOptionsbuilder;
        private VirbelaListingContext context;
        private UnitOfWork unitOfWork;

        public RegionRepositoryShould()
        {
            // ConnString for Dev Environment
            string encConnStr = 
                "RsJZctQGW8rsO2X/vhh7ewsDAKo8xDo7bEpjS7RwZFkq9KFLnlGEQLM9b3jGYARYVUINRxCTboYny3aWahtP7BHOew2ToMyxGDuO9BuYfpyZwH81uC883tyfXS2caR6rk0fTN1u/+dg05+L7sfLuDe8becDugt35NR2ahQEXCdVmHOs4JRAwWqvkL0EcqVVmwP4g1zUdfvg4yhzOtXVLmrf+xJFG6CFlCRw91hgUTCk5A6a2uPYHpKKiW7U0/cTZ6i9vKFqFJMvXxzRKU2hu3aMJ1iZsWgF3AR1jSwEOHQg=";
            connStr = AesCryptoUtil.Decrypt(encConnStr);
            dbOptionsbuilder = new DbContextOptionsBuilder<VirbelaListingContext>()
                                .UseSqlServer(connStr);
            context = new VirbelaListingContext(dbOptionsbuilder.Options);
            unitOfWork = new UnitOfWork(context);
        }

        [Fact]
        public async void ReturnNoneForInvalidParameters()
        {
            // Arrange: -1 is not a valid Region Name.
            object invalidParams = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("Name", "-1")
            };

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.RegionRepository.GetAsync(invalidParams);
                Assert.Empty(result);
            }
        }

        [Fact]
        public async void ReturnNoneForInvalidId()
        {
            // Arrange: ID column aut increases starting from 1
            string id = "-1";

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.RegionRepository.GetAsync(id);
                Assert.Null(result);
            }
        }

        [Fact]
        public async void ReturnRegionForValidId()
        {
            // Arrange: DB is seeded with at least one item.
            string id = "1";

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.RegionRepository.GetAsync(id);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void ReturnRegionForValidParameters()
        {
            // Arrange: DB is seeded with Region Name "A".
            object validParams = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("Name", "A")
            };

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.RegionRepository.GetAsync(validParams);
                Assert.Single(result);
            }
        }

    }

}