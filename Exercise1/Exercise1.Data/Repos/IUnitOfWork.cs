using Exercise1.Data.Models.VirbelaListing;

namespace Exercise1.Data.Repos {
    public interface IUnitOfWork {
        IRepository<Listing> ListingRepository { get; }
        IRepository<Listinguser> ListinguserRepository { get; }
        IRepository<Region> RegionRepository { get; }
        IRepository<Region_Listing> Region_ListingRepository { get; }

        void Commit();
        void Rollback();
    }
}