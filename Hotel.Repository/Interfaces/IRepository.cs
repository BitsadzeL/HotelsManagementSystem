using System.Linq.Expressions;

namespace Hotels.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task<List<T>> GetAllAsync(string includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
