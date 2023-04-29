using ClassWork.Domain.Entites;
using System.Linq.Expressions;

namespace ClassWork.Data.IRepositories
{
    public interface IRepository<TEntity>
    {
        public Task<TEntity> InsertAsync(TEntity entity);
        public Task<bool> DeleteAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
        public Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
        public IQueryable<TEntity> SelectAll(
            Expression<Func<TEntity,bool>> expression, string[] includes = null ,bool isTracking = true);

    }
}
