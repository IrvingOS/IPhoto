using IPhoto.Repositories;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Services.Impl
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected IBaseRepository<TEntity> _iBaseRepository;

        public async Task<bool> InsertAsync(TEntity entity)
        {
            return await _iBaseRepository.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _iBaseRepository.DeleteAsync(id);
        }
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _iBaseRepository.UpdateAsync(entity);
        }

        public async virtual Task<TEntity> GetAsync(string id)
        {
            return await _iBaseRepository.GetAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.GetAsync(func);
        }

        public async Task<List<TEntity>> ListAsync()
        {
            return await _iBaseRepository.ListAsync();
        }

        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.ListAsync(func);
        }

        public async virtual Task<List<TEntity>> PageAsync(int pageNum, int pageSize, RefAsync<int> total)
        {
            return await _iBaseRepository.PageAsync(pageNum, pageSize, total);
        }

        public async virtual Task<List<TEntity>> PageAsync(Expression<Func<TEntity, bool>> func, int pageNum, int pageSize, RefAsync<int> total)
        {
            return await _iBaseRepository.PageAsync(func, pageNum, pageSize, total);
        }

    }
}
