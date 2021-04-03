using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise1.Common.Tasks;
using Exercise1.Data.Models.VirbelaListing;
using Exercise1.Data.Repos;
using Exercise1.DataAccess.Context;
using Exercise1.DataAccess.Repos.Extension;
using System;

namespace Exercise1.DataAccess.Repos.VirbelaListing
{
    public class ListinguserRepository : IRepository<Listinguser>
    {
        private VirbelaListingContext _context;

        public ListinguserRepository(VirbelaListingContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<Listinguser>> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Listinguser> dbSet =  _context.Listinguser;
            IQueryable<Listinguser> query = dbSet.AsNoTracking();

            if (parameters != null) {
                var param = (List<KeyValuePair<string, string>>)parameters;

                foreach(KeyValuePair<string, string> kv in param) {
                    if (kv.Value == null)
                        continue;

                    if (kv.Key == "UserId") {
                        query = query.Where(i => i.Userid == kv.Value);
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

        public async Task<Listinguser> GetAsync(string id)
        {
            int idNum;
            if (!int.TryParse(id, out idNum))
                return await Task.FromResult<Listinguser>(null);

            IQueryable<Listinguser> result = _context.Listinguser
                .Where(r => r.Id == idNum);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Listinguser> PutAsync(string id, Listinguser updateRequest)
        {
            return await TaskConstants<Listinguser>.NotImplemented;
        }

        public async Task<Listinguser> PostAsync(Listinguser createRequest)
        {
            await _context.Listinguser.AddAsync(createRequest);
            // _context.Entry(createRequest).State = EntityState.Added;
            return createRequest;
        }

        public async Task<Listinguser> DeleteAsync(string id)
        {
            return await TaskConstants<Listinguser>.NotImplemented;
        }

    }
}