using System.Collections.Generic;
using System.Threading.Tasks;

namespace article_manager.api.Repositories
{
    public interface ICRUDRepository<T>
    {
        Task<IEnumerable<T>> GetList();
        Task<T> Get(int id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);

    }
}