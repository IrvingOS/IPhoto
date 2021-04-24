using IPhoto.Models;
using IPhoto.Repositories;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPhoto.Services.Impl
{
    public class UserLikeService : BaseService<UserLike>, IUserLikeService
    {
        private readonly IUserLikeRepository _iUserRepository;
        private readonly IUserService _iUserService;

        public UserLikeService(IUserLikeRepository userLikeRepository, IUserService userService)
        {
            base._iBaseRepository = userLikeRepository;
            _iUserRepository = userLikeRepository;
            _iUserService = userService;
        }

        public override Task<List<UserLike>> PageAsync(int pageNum, int pageSize, RefAsync<int> total)
        {
            var userLikeList = base.PageAsync(pageNum, pageSize, total);
            if (userLikeList != null)
            {
                foreach(UserLike userLike in userLikeList.Result)
                {
                    userLike.Photo.ApplicationUser = _iUserService.GetAsync(userLike.Photo.UserId).Result;
                }
            }
            return userLikeList;
        }

        public override Task<List<UserLike>> PageAsync(Expression<Func<UserLike, bool>> func, int pageNum, int pageSize, RefAsync<int> total)
        {
            var userLikeList = base.PageAsync(func, pageNum, pageSize, total);
            if (userLikeList != null)
            {
                foreach (UserLike userLike in userLikeList.Result)
                {
                    userLike.Photo.ApplicationUser = _iUserService.GetAsync(userLike.Photo.UserId).Result;
                }
            }
            return userLikeList;
        }
    }
}
