using IPhoto.Models;
using MySqlConnector;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Repositories.Impl
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context=null) : base(context)
        {
            base.Context = DbScoped.Sugar;

            // 创建数据库、创建表
            /*base.Context.DbMaintenance.CreateDatabase();
            base.Context.CodeFirst.SetStringDefaultLength(256).InitTables(
                typeof(ApplicationUser),
                typeof(Photo),
                typeof(File),
                typeof(UserLike)
               );*/
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public async Task<TEntity> GetAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public async Task<List<TEntity>> ListAsync()
        {
            return await base.GetListAsync();
        }

        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public async virtual Task<List<TEntity>> PageAsync(int pageNum, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .ToPageListAsync(pageNum, pageSize, total);
        }

        public async virtual Task<List<TEntity>> PageAsync(Expression<Func<TEntity, bool>> func, int pageNum, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .Where(func)
                .ToPageListAsync(pageNum, pageSize, total);
        }
    }
}
