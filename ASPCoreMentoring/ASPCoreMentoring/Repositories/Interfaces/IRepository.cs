using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities.Interfaces;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        IQueryable<TEntity> GetAllQueryable();

        Task<TEntity> GetByIdAsync(int id);

        Task UpdateAsync(TEntity entity);
    }
}
