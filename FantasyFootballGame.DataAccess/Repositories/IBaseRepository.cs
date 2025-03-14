
using System.Linq.Expressions;

namespace FantasyFootballGame.DataAccess.Repositories
{
    public interface IBaseRepository <T> where T : class
    {
        Task<T> GetById (int id);
        void Update (T entity);
        void Delete (T entity);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Save();
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetByIds(List<int> ids);

    }
}
