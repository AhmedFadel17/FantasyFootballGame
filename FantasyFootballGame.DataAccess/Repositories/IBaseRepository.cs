
namespace FantasyFootballGame.DataAccess.Repositories
{
    public interface IBaseRepository <T> where T : class
    {
        public Task<T> GetById (int id);
        public void UpdateById (T entity);
        public void DeleteById (T entity);
        public Task<IEnumerable<T>> GetAll();
        public void Create(T entity);
    }
}
