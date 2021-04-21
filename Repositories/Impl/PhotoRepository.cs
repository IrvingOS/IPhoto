using IPhoto.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Repositories.Impl
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public async override Task<List<Photo>> ListAsync()
        {
            return await base.Context.Queryable<Photo>()
                .Mapper(p => p.ApplicationUser, p => p.UserId, p => p.ApplicationUser.Id)
                .Mapper(p => p.File, p => p.FileId, p => p.File.Id)
                .OrderBy(p => p.CreateAt, OrderByType.Desc)
                .ToListAsync();
        }

        public async override Task<List<Photo>> ListAsync(Expression<Func<Photo, bool>> func)
        {
            return await base.Context.Queryable<Photo>()
                .Where(func)
                .Mapper(p => p.ApplicationUser, p => p.UserId, p => p.ApplicationUser.Id)
                .Mapper(p => p.File, p => p.FileId, p => p.File.Id)
                .OrderBy(p => p.CreateAt, OrderByType.Desc)
                .ToListAsync();
        }

        public async override Task<List<Photo>> PageAsync(int pageNum, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<Photo>()
                .Mapper(p => p.ApplicationUser, p => p.UserId, p => p.ApplicationUser.Id)
                .Mapper(p => p.File, p => p.FileId, p => p.File.Id)
                .ToPageListAsync(pageNum, pageSize, total);
        }

        public override Task<List<Photo>> PageAsync(Expression<Func<Photo, bool>> func, int pageNum, int pageSize, RefAsync<int> total)
        {
            return base.PageAsync(func, pageNum, pageSize, total);
        }
    }
}
