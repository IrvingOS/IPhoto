using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Services
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {

        Task<int> CountAsync(Expression<Func<TEntity, bool>> func);

        Task<bool> InsertAsync(TEntity entity);

        Task<bool> DeleteAsync(string id);

        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> func);

        Task<bool> UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync(string id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> func);

        Task<List<TEntity>> ListAsync();

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> func);

        Task<List<TEntity>> PageAsync(int pageNum, int pageSize, RefAsync<int> total);

        Task<List<TEntity>> PageAsync(Expression<Func<TEntity, bool>> func, int pageNum, int pageSize, RefAsync<int> total);
    }
}
