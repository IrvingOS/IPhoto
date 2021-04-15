using IPhoto.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Repositories.Impl
{
    public class UserLikeRepository : BaseRepository<UserLike>, IUserLikeRepository
    {
        public async override Task<List<UserLike>> PageAsync(int pageNum, int pageSize, RefAsync<int> total)
        {
            return await base.PageAsync(pageNum, pageSize, total);
        }

        public async override Task<List<UserLike>> PageAsync(Expression<Func<UserLike, bool>> func, int pageNum, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<UserLike>()
                .Where(func)
                .Mapper(u => u.Photo, u => u.PhotoId, u => u.Photo.Id)
                .ToPageListAsync(pageNum, pageSize, total);
        }
    }
}
