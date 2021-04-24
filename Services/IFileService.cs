using IPhoto.Models;
using System.Threading.Tasks;

namespace IPhoto.Services
{
    public interface IFileService : IBaseService<File>
    {
        Task<File> GetHeadPhotoByUserId(string userId);
    }
}
