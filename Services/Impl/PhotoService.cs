using IPhoto.Models;
using IPhoto.Repositories;

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
