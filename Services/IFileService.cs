using IPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Services
{
    public interface IFileService : IBaseService<File>
    {
        Task<File> GetHeadPhotoByUserId(string userId);
    }
}
