using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<bool> InsertAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync(int id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> func);

        Task<List<TEntity>> ListAsync();

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> func);

        Task<List<TEntity>> PageAsync(int pageNum, int pageSize, RefAsync<int> total);

        Task<List<TEntity>> PageAsync(Expression<Func<TEntity, bool>> func, int pageNum, int pageSize, RefAsync<int> total);

    }
}
