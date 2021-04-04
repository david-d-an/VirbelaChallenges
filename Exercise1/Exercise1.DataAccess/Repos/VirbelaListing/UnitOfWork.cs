using System;
using Exercise1.Data.Models.VirbelaListing;
using Exercise1.Data.Repos;
using Exercise1.DataAccess.Context;

namespace Exercise1.DataAccess.Repos.VirbelaListing
{
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private readonly VirbelaListingContext _virbelaListingContext;
        private IRepository<Listing> _listingRepository;
        private IRepository<Listinguser> _listingusersRepository;
        private IRepository<Region> _regionRepository;
        private IRepository<Region_Listing> _region_ListingRepository;

        public UnitOfWork() { }

        public UnitOfWork(VirbelaListingContext virbelaListingContext) { 
            _virbelaListingContext = virbelaListingContext; 
        }

        public IRepository<Listing> ListingRepository {
            get { return _listingRepository = 
                _listingRepository ?? 
                new ListingRepository(_virbelaListingContext);
            }
        }

        public IRepository<Listinguser> ListinguserRepository {
            get { return _listingusersRepository = 
                _listingusersRepository ?? 
                new ListinguserRepository(_virbelaListingContext);
            }
        }

        public IRepository<Region> RegionRepository {
            get { return _regionRepository = 
                _regionRepository ?? 
                new RegionRepository(_virbelaListingContext);
            }
        }

        public IRepository<Region_Listing> Region_ListingRepository {
            get { return _region_ListingRepository = 
                _region_ListingRepository ?? 
                new Region_ListingRepository(_virbelaListingContext);
            }
        }

        public void Commit() { 
            _virbelaListingContext.SaveChanges();
        }

        public void Rollback() { 
            _virbelaListingContext.Dispose(); 
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _virbelaListingContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
