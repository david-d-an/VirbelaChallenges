using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise1.Data.Models.VirbelaListing;
using Exercise1.Data.Repos;
using Exercise1.DataAccess.Context;
using System;
using Exercise1.Common.Tasks;

namespace Exercise1.DataAccess.Repos.VirbelaListing
{
    public class Region_ListingRepository : IRepository<Region_Listing>
    {
        private VirbelaListingContext _context;

        public Region_ListingRepository(VirbelaListingContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<Region_Listing>> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            var query = _context.Listing
                .Include(i => i.Creator)
                .ThenInclude(i => i.Region)
                .AsNoTracking()
                .Select(r => new Region_Listing {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Price = r.Price,
                    CreatorId = r.CreatorId,
                    CreatedDate = r.CreatedDate,
                    RegionId = r.Creator.RegionId,
                    RegionName = r.Creator.Region.Name
                });

            if (parameters != null) {
                var param = (List<KeyValuePair<string, string>>)parameters;

                // TO DO: Improve the loop by Reflection later
                // TO DO: Extract filtering logics in separte module
                foreach(KeyValuePair<string, string> kv in param) {
                    if (kv.Value == null)
                        continue;
                    else if (kv.Key == "CreatorId" 
                    && int.TryParse(kv.Value, out int creatorId)) {
                        query = query.Where(i => i.CreatorId == creatorId);
                    }
                    else if (kv.Key == "RegionId" 
                    && int.TryParse(kv.Value, out int regionId)) {
                        query = query.Where(i => i.RegionId == regionId);
                    }
                    else if (kv.Key == "Title") {
                        query = query.Where(i => i.Title == kv.Value);
                    }
                    else if (kv.Key == "Description") {
                        query = query.Where(i => i.Description == kv.Value);
                    }
                    else if (kv.Key == "Price") {
                        if (!decimal.TryParse(kv.Value, out decimal price))
                            return new List<Region_Listing>();
                        query = query.Where(i => i.Price == price);
                    }
                    else if (kv.Key == "CreatedDate") {
                        if (!DateTime.TryParse(kv.Value, out DateTime date)) 
                            return new List<Region_Listing>();
                        query = query.Where(i => i.CreatedDate == date);
                    }
                }
            }

            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Region_Listing> GetAsync(string id)
        {
            int idNum;
            if (!int.TryParse(id, out idNum))
                return await Task.FromResult<Region_Listing>(null);

            var query = _context.Listing
                .Include(i => i.Creator)
                .ThenInclude(i => i.Region)
                .AsNoTracking()
                .Select(r => new Region_Listing {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Price = r.Price,
                    CreatorId = r.CreatorId,
                    CreatedDate = r.CreatedDate,
                    RegionId = r.Creator.RegionId,
                    RegionName = r.Creator.Region.Name
                })
                .Where(r => r.Id == idNum);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Region_Listing> PutAsync(
            string id, 
            Region_Listing updateRequest)
        {
            return await TaskConstants<Region_Listing>.NotImplemented;
        }

        public async Task<Region_Listing> PostAsync(
            Region_Listing createRequest)
        {
            return await TaskConstants<Region_Listing>.NotImplemented;
        }

        public async Task<Region_Listing> DeleteAsync(string id)
        {
            return await TaskConstants<Region_Listing>.NotImplemented;
        }
    }
}