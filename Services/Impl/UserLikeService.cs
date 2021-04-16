using IPhoto.Models;
using IPhoto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Services.Impl
{
    public class UserLikeService : BaseService<UserLike>, IUserLikeService
    {
        private readonly IUserLikeRepository _iUserRepository;

        public UserLikeService(IUserLikeRepository userLikeRepository)
        {
            base._iBaseRepository = userLikeRepository;
            _iUserRepository = userLikeRepository;
        }
    }
}
