using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise1.Common.Tasks;
using Exercise1.Data.Models.VirbelaListing;
using Exercise1.Data.Repos;
using Exercise1.DataAccess.Context;

namespace Exercise1.DataAccess.Repos.VirbelaListing
{
    public class RegionRepository : IRepository<Region>
    {
        private VirbelaListingContext _context;

        public RegionRepository(VirbelaListingContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<Region>> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Region> dbSet =  _context.Region;
            IQueryable<Region> query = dbSet.AsNoTracking();

            if (parameters != null) {
                var param = (List<KeyValuePair<string, string>>)parameters;

                foreach(KeyValuePair<string, string> kv in param) {
                    if (kv.Value == null)
                        continue;

                    if (kv.Key == "Name") {
                        query = query.Where(i => i.Name == kv.Value);
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

        public async Task<Region> GetAsync(string id)
        {
            int idNum;
            if (!int.TryParse(id, out idNum))
                return await Task.FromResult<Region>(null);

            IQueryable<Region> result = _context.Region
                .Where(r => r.Id == idNum);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Region> PutAsync(string id, Region updateRequest)
        {
            return await TaskConstants<Region>.NotImplemented;
        }

        public async Task<Region> PostAsync(Region createRequest)
        {
            return await TaskConstants<Region>.NotImplemented;
        }

        public async Task<Region> DeleteAsync(string id)
        {
            return await TaskConstants<Region>.NotImplemented;
        }

    }
}