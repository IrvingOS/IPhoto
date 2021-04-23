using IPhoto.Models;
using IPhoto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Services.Impl
{
    public class FileService : BaseService<File>, IFileService
    {
        private readonly IFileRepository _iFileRepository;
        private readonly IUserService _iUserService;

        public FileService(IFileRepository fileRepository, IUserService userService)
        {
            base._iBaseRepository = fileRepository;
            _iFileRepository = fileRepository;
            _iUserService = userService;
        }

        public async Task<File> GetHeadPhotoByUserId(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            ApplicationUser user = _iUserService.GetAsync(userId).Result;
            if (user.HeadPhotoFileId != null)
            {
                return await base.GetAsync(user.HeadPhotoFileId);
            }
            else
            {
                return user.Gender switch
                {
                    0 => await base.GetAsync("0"),
                    1 => await base.GetAsync("1"),
                    _ => null,
                };
            }
        }
    }
}
