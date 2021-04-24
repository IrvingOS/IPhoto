using IPhoto.Models;
using IPhoto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static IPhoto.Models.PhotoInputModel;

namespace IPhoto.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPhotoService _iPhotoService;
        private readonly IUserLikeService _iUserLikeService;
        private readonly IUserService _iUserService;
        private readonly IFileService _iFileService;

        public UserController(UserManager<ApplicationUser> userManager, 
            IPhotoService photoService, 
            IUserLikeService userLikeService,
            IUserService userService,
            IFileService fileService)
        {
            this._userManager = userManager;
            this._iPhotoService = photoService;
            this._iUserLikeService = userLikeService;
            this._iUserService = userService;
            this._iFileService = fileService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UploadModel Upload { get; set; }

        public IActionResult Index()
        {
            ViewBag.StatusMessage = StatusMessage;
            ViewBag.PhotoList = _iPhotoService.ListAsync(p => p.UserId == _userManager.GetUserId(User)).Result;
            ViewBag.PhotoCount = 0;
            ViewBag.LikeCount = 0;
            ViewBag.DownloadCount = 0;

            foreach(var photo in ViewBag.PhotoList)
            {
                ViewBag.PhotoCount++;
                ViewBag.LikeCount += photo.LikeCount;
                ViewBag.DownloadCount += photo.DownloadCount;
            }

            return View();
        }

        public IActionResult Collection()
        {
            ViewBag.StatusMessage = StatusMessage;
            ViewBag.LikeList = _iUserLikeService.ListAsync(u => u.UserId == _userManager.GetUserId(User)).Result;
            ViewBag.LikeCount = 0;

            foreach (var like in ViewBag.LikeList)
            {
                ViewBag.LikeCount++;
                like.Photo.File = _iFileService.GetAsync(like.Photo.FileId).Result;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(UploadModel PhotoForm)
        {
            Photo photo = new();
            photo.Id = "photo" + System.Guid.NewGuid().ToString()[5..];
            photo.UserId = _userManager.GetUserId(User);
            photo.Title = PhotoForm.Title;
            photo.FileId = PhotoForm.FileId;
            photo.LikeCount = 0;
            photo.DownloadCount = 0;
            photo.CreateAt = DateTime.Now;

            Console.WriteLine(photo.ToString());

            var result = await _iPhotoService.InsertAsync(photo);
            Console.WriteLine(result.ToString());
            if (result)
            {
                StatusMessage = "图片上传成功！";
            }
            else
            {
                StatusMessage = "Error!图片上传失败！";
            }
            return RedirectToAction(actionName: nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> DeletePhoto(string id)
        {
            var result = await _iPhotoService.DeleteAsync(id);
            if (await _iUserLikeService.CountAsync(u => u.PhotoId == id) != 0)
            {
            
                result &= await _iUserLikeService.DeleteAsync(u => u.PhotoId == id);
            }
            if (result)
            {
                StatusMessage = "图片删除成功！";
            }
            else
            {
                StatusMessage = "Error!图片删除失败！";
            }
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLike(string id)
        {
            var userLike = await _iUserLikeService.GetAsync(id);
            var photo = await _iPhotoService.GetAsync(userLike.PhotoId);
            photo.LikeCount--;

            var result = await _iUserLikeService.DeleteAsync(id) && await _iPhotoService.UpdateAsync(photo);

            if (result)
            {
                StatusMessage = "图片删除成功！";
            }
            else
            {
                StatusMessage = "Error!图片删除失败！";
            }

            return RedirectToAction(actionName: nameof(Collection));
        }
    }
}
