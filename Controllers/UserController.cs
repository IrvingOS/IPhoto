using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IPhoto.Models;
using System.ComponentModel.DataAnnotations;
using static IPhoto.Models.PhotoInputModel;
using IPhoto.Services;
using IPhoto.Common.ApiResult;

namespace IPhoto.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPhotoService _iPhotoService;

        public UserController(UserManager<ApplicationUser> userManager, IPhotoService photoService)
        {
            this._userManager = userManager;
            this._iPhotoService = photoService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UploadModel Upload { get; set; }

        public IActionResult Index()
        {
            ViewBag.StatusMessage = StatusMessage;
            return View();
        }

        public IActionResult Collection()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(UploadModel PhotoForm)
        {
            Console.WriteLine(PhotoForm.ToString());

            /*if (!ModelState.IsValid)
            {
                return Page();
            }
*/
            Photo photo = new();
            photo.Id = System.Guid.NewGuid().ToString();
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
    }
}
