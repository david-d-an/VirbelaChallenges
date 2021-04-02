using System.Linq;
using Exercise1.Common.Security;
using Exercise1.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Exercise1.DataAccess.Context
{
    public class VirbelaListingContextTest
    {
        private string connStr;
        private DbContextOptionsBuilder<VirbelaListingContext> dbOptionsbuilder;
        private VirbelaListingContext context;

        public VirbelaListingContextTest()
        {
            string encConnStr = 
                "RsJZctQGW8rsO2X/vhh7ewsDAKo8xDo7bEpjS7RwZFkq9KFLnlGEQLM9b3jGYARYVUINRxCTboYny3aWahtP7BHOew2ToMyxGDuO9BuYfpyZwH81uC883tyfXS2caR6rk0fTN1u/+dg05+L7sfLuDe8becDugt35NR2ahQEXCdVmHOs4JRAwWqvkL0EcqVVmwP4g1zUdfvg4yhzOtXVLmrf+xJFG6CFlCRw91hgUTCk5A6a2uPYHpKKiW7U0/cTZ6i9vKFqFJMvXxzRKU2hu3aMJ1iZsWgF3AR1jSwEOHQg=";
            connStr = AesCryptoUtil.Decrypt(encConnStr);
            dbOptionsbuilder = new DbContextOptionsBuilder<VirbelaListingContext>()
                                .UseSqlServer(connStr);
            context = new VirbelaListingContext(dbOptionsbuilder.Options);
        }

        [Fact]
        public void SampleTest()
        {

        }
    }
}