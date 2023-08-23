using Core6_JWT_Login.Models;
using System.Linq.Expressions;

namespace Core6_JWT_Login.Repos
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllReadOnly();

        bool Any(Expression<Func<T, bool>> expression);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression);

        T GetById<Tid>(Tid id);
        Task<T> GetByIdAsnyc<Tid>(Tid id);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

    }
    
}
