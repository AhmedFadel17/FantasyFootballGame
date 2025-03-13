
using System.Linq.Expressions;

namespace FantasyFootballGame.DataAccess.Repositories
{
    public interface IBaseRepository <T> where T : class
    {
        public Task<T> GetById (int id);
        public void Update (T entity);
        public void Delete (T entity);
        public Task<IEnumerable<T>> GetAll();
        public Task Create(T entity);
        public Task Save();
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
