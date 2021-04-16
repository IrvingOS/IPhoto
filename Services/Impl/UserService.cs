using IPhoto.Models;
using IPhoto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Services.Impl
{
    public class UserService : BaseService<ApplicationUser>, IUserService
    {
        private readonly IUserRepository _iUserRepository;

        public UserService(IUserRepository iUserRepository)
        {
            base._iBaseRepository = iUserRepository;
            _iUserRepository = iUserRepository;
        }
    }
}
