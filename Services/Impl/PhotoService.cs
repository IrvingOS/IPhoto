using IPhoto.Models;
using IPhoto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Services.Impl
{
    public class PhotoService : BaseService<Photo>, IPhotoService
    {
        private readonly IPhotoRepository _iPhotoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            base._iBaseRepository = photoRepository;
            _iPhotoRepository = photoRepository;
        }
    }
}
