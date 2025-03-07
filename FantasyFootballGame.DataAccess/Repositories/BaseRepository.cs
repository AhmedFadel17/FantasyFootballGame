using FantasyFootballGame.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
        }
        public async void Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void DeleteById(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void UpdateById(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
