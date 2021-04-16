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

        public FileService(IFileRepository fileRepository)
        {
            base._iBaseRepository = fileRepository;
            _iFileRepository = fileRepository;
        }
    }
}
