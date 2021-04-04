using Xunit;
using Exercise1.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Exercise1.Common.Security;
using Exercise1.DataAccess.Repos.VirbelaListing;
using System.Collections.Generic;

namespace Exercise1.DataAccess.Repos
{
    public class ListingRepositoryShould
    {
        private string connStr;
        private DbContextOptionsBuilder<VirbelaListingContext> dbOptionsbuilder;
        private VirbelaListingContext context;

        public ListingRepositoryShould()
        {
            // Development Setting
            // Server=tcp:virbelalisting.database.windows.net,1433;
            // Initial Catalog=VirbelaListingDev;
            // Persist Security Info=False;
            // User ID=appuser;
            // Password={password};
            // MultipleActiveResultSets=False;
            // Encrypt=True;TrustServerCertificate=False;
            string encConnStr = 
                "RsJZctQGW8rsO2X/vhh7ewsDAKo8xDo7bEpjS7RwZFkq9KFLnlGEQLM9b3jGYARYVUINRxCTboYny3aWahtP7BHOew2ToMyxGDuO9BuYfpxmNDCVRydZ5efJTTL2O9FkiOGrbqlILlQPt5/8DcwssjosrrVeyxXrgIHB7pIN48IPOLp29HxT67vWGovw4jt+QtegcVynARe8g9XbGU6dB57kDogQ5t33I5iovM52B1o8tzRuYekLE/std6JtXC7McwscfvTKSE+85Woq7ljaLP6k5pRx83QaMvCe6Y7ICdAc5oKTzODrVpEZ+ae3uhaR";
            connStr = AesCryptoUtil.Decrypt(encConnStr);
            dbOptionsbuilder = new DbContextOptionsBuilder<VirbelaListingContext>()
                                .UseSqlServer(connStr);
            context = new VirbelaListingContext(dbOptionsbuilder.Options);
        }

        [Fact]
        public async void ReturnNoneForInvalidParameters()
        {
            // Arrange: Varchar columns shall not be populated with -1 only. 
            //          Price shall be positve decimal.
            //          User.Id shall be 1 or larger
            object invalidParams = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("CreatorId", "-1"),
                new KeyValuePair<string, string> ("Title", "-1" ),
                new KeyValuePair<string, string> ("Description", "-1" ),
                new KeyValuePair<string, string> ("Price", "-1")
            };

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(invalidParams);
                Assert.Empty(result);
            }
        }

        [Fact]
        public async void ReturnNoneForInvalidId()
        {
            // Arrange: DB is seeded with at least one item.
            string id = "-1";

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(id);
                Assert.Null(result);
            }
        }

        /* This test interacts with database.                            */
        /* Therere, it's susceptible to failure in case of data changes. */
        /* It's best to keep this test inactive immediatel after         */
        /* the test passes.                                              */
        [Fact]
        public async void ReturnListingForValidId()
        {
            // Arrange: DB is seeded with at least one item.
            string id = "1";

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(id);
                Assert.NotNull(result);
            }
        }

        /* This test interacts with database.                            */
        /* Therere, it's susceptible to failure in case of data changes. */
        /* It's best to keep this test inactive immediatel after         */
        /* the test passes.                                              */
        [Fact]
        public async void ReturnListingForValidParameters()
        {
            // Arrange: Varchar columns shall not be populated with -1 only. 
            //          Price shall be positve decimal.
            //          User.Id shall be 1 or larger
            object validParams = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("CreatorId", "1"),
                new KeyValuePair<string, string> ("Title", "Listing A" ),
                // new KeyValuePair<string, string> ("Description", "-1" ),
                new KeyValuePair<string, string> ("Price", "12.34"),
                new KeyValuePair<string, string> ("CreatedDate", "03/21/2021 09:15:22 AM")
            };

            // Act & Assert
            using (var uow = new UnitOfWork(context)) {
                var result = await uow.ListingRepository.GetAsync(validParams);
                Assert.Single(result);
            }
        }
    }

}