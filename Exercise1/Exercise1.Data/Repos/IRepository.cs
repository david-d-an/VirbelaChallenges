using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise1.Data.Repos
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(
            object parameters = null, 
            int? pageNum = null, int? 
            pageSize = null);
        Task<T> GetAsync(string id);
        Task<T> PutAsync(string id, T updateRequest);
        Task<T> PostAsync(T createRequest);
        Task<T> DeleteAsync(string id);
    }
}