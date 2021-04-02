using Exercise1.Data.Models.VirbelaListing;

namespace Exercise1.Data.Repos {
    public interface IUnitOfWork {
        IRepository<Listing> ListingRepository { get; }
        IRepository<Listinguser> ListinguserRepository { get; }
        IRepository<Region> RegionRepository { get; }

        void Commit();
        void Rollback();
    }
}