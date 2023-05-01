using System.Collections.Generic;
using System.Linq.Expressions;

namespace JamesMoonPortfolioRedux.Data.IRepository
{
    public interface IProfileRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string orderString = null, string? includeProperties = null, bool tracked = true);
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>>? filter = null, string orderString = null, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string orderString = null, string? includeProperties = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
